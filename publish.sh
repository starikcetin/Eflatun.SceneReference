#!/bin/bash

PACKAGE_ROOT="Eflatun.SceneReference/Packages/com.eflatun.scenereference"
echo "> PACKAGE_ROOT: $PACKAGE_ROOT"

# Make sure git is clean
if [[ `git status --porcelain` ]]; then
  echo "> There are outstanding changes. Refusing to publish."
  read -p ">> Press any key to exit..."
  
  exit 1
fi

read -p "> Make sure Unity is open. Then press any key to continue..."

BASE_COMMIT_HASH="$(git rev-parse HEAD)"
BASE_COMMIT_MESSAGE="$(git log -1 --pretty=%B)"
echo "> BASE_COMMIT_HASH: $BASE_COMMIT_HASH"
echo "> BASE_COMMIT_MESSAGE: $BASE_COMMIT_MESSAGE"
read -p "> Make sure this looks OK. Then press any key to continue..."

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
cp -r ".assets" "$PACKAGE_ROOT"

read -p "> Go to Unity and wait for it to finish whatever it is doing. Then close Unity. Then press any key to continue..."

# make a commit
echo "> Make a commit for copied files"
git add .
git commit -m "chore: copy readme, changelog, license, and assets to package directory"

COPY_COMMIT_MESSAGE="$(git log -1 --pretty=%B)"
echo "> COPY_COMMIT_MESSAGE: $COPY_COMMIT_MESSAGE"

# run gup
echo "> Run git-upm-publisher"
gup -p "$PACKAGE_ROOT/package.json"

GUP_COMMIT_MESSAGE="$(git log -1 --pretty=%B)"
echo "> GUP_COMMIT_MESSAGE: $GUP_COMMIT_MESSAGE"

# squash commits
echo "> Squashing commits to base"
git reset --soft $BASE_COMMIT_HASH
git commit --amend -m "$BASE_COMMIT_MESSAGE" -m "* $COPY_COMMIT_MESSAGE" -m "* $GUP_COMMIT_MESSAGE"

# done
echo "> Done"
read -p ">> Press any key to exit..."
