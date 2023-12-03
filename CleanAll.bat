@echo off
cd /d "%~dp0"
echo [Current Directory]
echo "%CD%"
pause
echo 1/3 ".\.vs" folder deleting...
rmdir /s /q ".\.vs"
echo 2/3 ".\WpfSegoeFluentIconsFont\bin" folder deleting...
rmdir /s /q ".\WpfSegoeFluentIconsFont\bin"
echo 3/3 ".\WpfSegoeFluentIconsFont\obj" folder deleting...
rmdir /s /q ".\WpfSegoeFluentIconsFont\obj"
pause
