#!/bin/bash

MAIN_BRANCH_NAME="main"
TEMP_BRANCH_NAME="TEMP_BRANCH_3427892498"
PACKAGE_ROOT="Eflatun.SceneReference/Packages/com.eflatun.scenereference"

echo "> MAIN_BRANCH_NAME: $MAIN_BRANCH_NAME"
echo "> TEMP_BRANCH_NAME: $TEMP_BRANCH_NAME"
echo "> PACKAGE_ROOT: $PACKAGE_ROOT"

# Make sure git is clean
if [[ `git status --porcelain` ]]; then
  echo "> There are outstanding changes. Refusing to publish."
  read -p ">> Press any key to exit..."
  
  exit 1
fi

BASE_COMMIT_HASH="$(git rev-parse HEAD)"
echo "> BASE_COMMIT_HASH: $BASE_COMMIT_HASH"

# install gup
echo "> Ensure programs"
echo ">> Remove python git-upm-publisher"
pip uninstall git-upm-publisher
echo ">> Install npm git-upm-publisher"
npm i -g git-upm-publisher

# copy files to package dir
echo "> Copy essentail files to package folder"
cp "README.md" "$PACKAGE_ROOT/README.md"
cp "CHANGELOG.md" "$PACKAGE_ROOT/CHANGELOG.md"
cp "LICENSE.md" "$PACKAGE_ROOT/LICENSE.md"
cp -r ".assets" "$PACKAGE_ROOT/.assets"

# make a commit
echo "> Make a commit for copied files"
git add .
git commit -m "TEMP_COMMIT_5839058930"

# run gup
echo "> Run gup"
gup -p "$PACKAGE_ROOT/package.json"

COMMIT_HASH_TO_KEEP=$(git rev-parse HEAD)
echo "> COMMIT_HASH_TO_KEEP: $COMMIT_HASH_TO_KEEP"

# revert the copy commit
echo "> Undo the copy file commit"
echo ">> Checkout base"
git checkout $BASE_COMMIT_HASH
echo ">> Create temp branch"
git checkout -b $TEMP_BRANCH_NAME
echo ">> Cherry-pick version bump commit"
git cherry-pick $COMMIT_HASH_TO_KEEP
echo ">> Checkout main branch"
git checkout $MAIN_BRANCH_NAME
echo ">> Reset to temp branch"
git reset --hard $TEMP_BRANCH_NAME
echo ">> Delete temp branch"
git branch -d $TEMP_BRANCH_NAME

# rewrite the commit message
echo "> Rewrite commit message"
NEW_COMMIT_MESSAGE="chore: $(git log -1 --pretty=%B | tr '[:upper:]' '[:lower:]')"
echo ">> NEW_COMMIT_MESSAGE: $NEW_COMMIT_MESSAGE"
git commit --amend -m "$NEW_COMMIT_MESSAGE"

# done
echo "> Done"
read -p ">> Press any key to exit..."
