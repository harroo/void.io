
#this script may only work on MY machine, so configure it if you need
#the script assumes unity editor is installed in /opt

echo "Building Linux Player..."
/opt/Unity/2020.1.9f1/Editor/Unity -quit -batchmode -buildLinux64Player ~/Downloads/void-builds/linux-build/void.x86_64 -projectpath void-project

echo "Building Windows Player..."
/opt/Unity/2020.1.9f1/Editor/Unity -quit -batchmode -buildWindows64Player ~/Downloads/void-builds/windows-build/void.exe -projectpath void-project

cd ~/Downloads/void-builds/

echo "Compressing Linux Player Build..."
tar czvf linux.tar.xz linux-build/

echo "Compressing Windows Player Build..."
zip -r windows.zip windows-build/

nautilus ~/Downloads/void-builds/ & disown

echo "Done!"
