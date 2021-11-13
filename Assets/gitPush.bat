@echo off

SET /p Comment = "Enter a comment: "

git add ./
git pull
git push --repo https://github.com/B33bo/AZgame.git
git commit -m %Comment% -u

SET /p dummy = "finished."