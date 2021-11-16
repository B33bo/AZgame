@echo off

set /p msg=Enter Message: 

git add ./
git pull
git commit -u -m "%msg%"
git push --repo https://github.com/B33bo/AZgame.git

SET /p dummy = "finished."