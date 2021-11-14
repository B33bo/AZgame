@echo off

set /p msg=Enter Message: 

git add ./
git pull
git push --repo https://github.com/B33bo/AZgame.git
git commit -u -m "comment=%msg%"

SET /p dummy = "finished."