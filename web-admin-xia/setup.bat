@echo off


for /f "tokens=2 delims=:" %%a in ('ipconfig^|findstr "IPv4"') do (

 set IP=%%a

)

echo var baseUrl='http://%IP:~1%:58620';export {baseUrl} > ./config/ipconfig.js


start "" "./node-v8.1.2-x86.msi"
pause

echo set path=%path%;C:\Program Files (x86)\Nodejs

echo set path=%path%;C:\Program Files (x86)\nodejs

echo set path=%path%;C:\Program Files\Nodejs

echo set path=%path%;C:\Program Files\nodejs


exit