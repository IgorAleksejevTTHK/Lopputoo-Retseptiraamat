#!/usr/bin/env bash
# Creates a GitHub Project (v2) board and adds the existing backlog issues (#1-#14) to it.
# Labels, milestones and issues already exist in the repository.
# Requirements: gh CLI logged in with the project scope:
#   gh auth login -s project
# Usage: ./scripts/create-backlog.sh
set -euo pipefail

REPO="IgorAleksejevTTHK/Lopputoo-Retseptiraamat"
OWNER="IgorAleksejevTTHK"
PROJECT_TITLE="Retseptid ja toiduplaan"

PROJECT_NUMBER=$(gh project create --owner "$OWNER" --title "$PROJECT_TITLE" --format json --jq .number)
gh project link "$PROJECT_NUMBER" --owner "$OWNER" --repo "$REPO" >/dev/null || true

for n in $(gh issue list --repo "$REPO" --state all --limit 100 --json number --jq '.[].number'); do
  gh project item-add "$PROJECT_NUMBER" --owner "$OWNER" --url "https://github.com/$REPO/issues/$n" >/dev/null
  echo "added #$n"
done

echo "Project #$PROJECT_NUMBER: https://github.com/users/$OWNER/projects/$PROJECT_NUMBER"
