@echo off

SET /p Comment = "Enter a comment: "

git add .
git pull
git push --repo https://github.com/B33bo/AZgame.git

SET /p dummy = "finished."