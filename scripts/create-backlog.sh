#!/usr/bin/env bash
# Creates the GitHub Project board "Retseptid ja toiduplaan" with custom fields
# (Epic, Hinnang (h), Tähtaeg) and fills them from scripts/backlog.tsv.
# Issues, labels and milestones already exist in the repository.
#
# Run once in a Codespace of this repository:
#   unset GITHUB_TOKEN
#   gh auth login -s project
#   ./scripts/create-backlog.sh
set -euo pipefail

REPO="IgorAleksejevTTHK/Lopputoo-Retseptiraamat"
OWNER="IgorAleksejevTTHK"
TITLE="Retseptid ja toiduplaan"
TSV="$(dirname "$0")/backlog.tsv"

echo "Creating project..."
NUMBER=$(gh project create --owner "$OWNER" --title "$TITLE" --format json --jq .number)
PID=$(gh project view "$NUMBER" --owner "$OWNER" --format json --jq .id)
gh project link "$NUMBER" --owner "$OWNER" --repo "$REPO" >/dev/null || true

EPICS=$(grep -v '^#' "$TSV" | cut -f5 | sort -u | paste -sd, -)
gh project field-create "$NUMBER" --owner "$OWNER" --name "Epic" --data-type SINGLE_SELECT --single-select-options "$EPICS" >/dev/null
gh project field-create "$NUMBER" --owner "$OWNER" --name "Hinnang (h)" --data-type NUMBER >/dev/null
gh project field-create "$NUMBER" --owner "$OWNER" --name "Tähtaeg" --data-type DATE >/dev/null

FIELDS=$(gh project field-list "$NUMBER" --owner "$OWNER" --format json --limit 50)
fid()  { jq -r --arg n "$1" '.fields[] | select(.name==$n) | .id' <<<"$FIELDS"; }
opt()  { jq -r --arg n "$1" --arg o "$2" '.fields[] | select(.name==$n) | .options[] | select(.name==$o) | .id' <<<"$FIELDS"; }
EPIC_F=$(fid "Epic"); HOURS_F=$(fid "Hinnang (h)"); DUE_F=$(fid "Tähtaeg"); STATUS_F=$(fid "Status")

grep -v '^#' "$TSV" | while IFS=$'\t' read -r num title labels ms epic hours due state; do
  ITEM=$(gh project item-add "$NUMBER" --owner "$OWNER" --url "https://github.com/$REPO/issues/$num" --format json --jq .id)
  gh project item-edit --project-id "$PID" --id "$ITEM" --field-id "$EPIC_F" --single-select-option-id "$(opt Epic "$epic")" >/dev/null
  gh project item-edit --project-id "$PID" --id "$ITEM" --field-id "$HOURS_F" --number "$hours" >/dev/null
  gh project item-edit --project-id "$PID" --id "$ITEM" --field-id "$DUE_F" --date "$due" >/dev/null
  status=$([ "$state" = closed ] && echo Done || echo Todo)
  gh project item-edit --project-id "$PID" --id "$ITEM" --field-id "$STATUS_F" --single-select-option-id "$(opt Status "$status")" >/dev/null
  echo "added #$num $title"
done

echo
echo "Done: https://github.com/users/$OWNER/projects/$NUMBER"
echo "In the web UI: switch the view to Table, Group by -> Milestone,"
echo "and show the columns Labels, Milestone, Closed, Epic, Hinnang (h), Tähtaeg, Linked pull requests."
