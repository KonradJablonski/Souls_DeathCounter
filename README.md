# Death Counter for Souls games
Useful for Twitch streaming.
Developed by CodeRad

If you found this app useful, feel free to support my work.
<p><a href="https://www.paypal.me/KonradJablonski" target="_blank">DONATE</a></p>

Special thanks to cisc0disco

Tested on: Windows 10 64bit

<p><a href="https://drive.google.com/file/d/1Q4HwfYyeT9CugoCZBBgBlpZ4iTeaAO5p/preview" target="_blank">Example Image</a></p>

This application only supports:
<ol>
<li>Dark Souls: Prepare To Die</li>
<li>Dark Souls Remastered</li>
<li>Dark Souls 2</li>
<li>Dark Souls 2 Scholar of the first sin</li>
<li>Dark Souls 3</li>
<li>Sekiro</li>
</ol>
<b>Info:</b>
The idea behind this software is to run this along side your twitch stream where it will report automatically your death count.
The death count is retrived from RAM. As this application only READS the information, it will not be picked up by anti-cheat system.
Reading directly from RAM requires Admin rights, so you will be required to run this application as an admin.

<b>How To Use:</b>
1. Run the application.
2. Run your game load your save. 
3. Get death count from the app screen, or you can use deaths.txt. This text file is updated at the same time as the screen.
4. Go to Settings.txt to modifiy Font Color, Font Size, Font Type and Font Style of the death counter text.

<b>StreamLabs: </b>
1. Run the "Universal_Game_DeathCounter" app.
2. Run StreamLabs.
3. In StreamLabs, add new Source "Window Capture"
4. Under Window, select the "Universal_Game_DeathCounter" app.
5. Untick "Capture Cursor" and press Done.
6. Now you need to get rid of the gray box surrounding the text. 
7. Right Click on the just created source and select "Filters"
8. Add new Filter type "Color Key"
9. Select custom color. The background color is in RGB "64,64,64" or in hex 404040
10. Tweak anyother settings to your liking.
11. Press Done.
Now you should have transparent death counter. Next you can put a custom text next to it, something like "DEATHS".

<b>FAQ:</b>

<b>Do I need to start fresh run.</b>
* No. You do not need to start fresh game with this software, you can just start running it mid run and it will know your true death count.

<B>Can I change font color, or font type?</b>
* Yes. check "Settings.txt" file for more information.

<b>Do I need to Capture the application screen in order to get the death count?</b>
* No, there is also deaths.txt file that is updated at the same time. 

<b>Can I minimize the application?</b>
* Yes, from my testing it is still updating the death count. If it doesnt, just unminimize it. 

<b>What is the background color for transparency?</b>
* RGB "64,64,64" or in hex 404040

<b>Do I need to save the death count once I finish my session?</b>
* No.

<b>Can I download a custom font and use it in this app?</b>
* Yes, just need to make sure you write the "font name" in exactly the same way.

<b>DOWNLOAD:</b>
<p><a href="https://drive.google.com/file/d/1Pfadc6U2pCSDthqK4_OchblXczQdnoI4/view?usp=sharing">Download Latest Version (1.0.0.1)</a></p>
