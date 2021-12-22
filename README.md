# HackathonLearnLive

[![SC2 Video](https://raw.githubusercontent.com/DavidEggenberger/HackathonLearnLive/master/LearnLive.PNG)](https://youtu.be/jq-n3eMEs_M "Click to Watch a walkthrough")
Click on the screenshot to see a walkthrough on YouTube.


<h3>What does it do?</h3>

Users can sign up either with their Google or GitHub accounts. They can create learning groups themselves or join groups that have already been created. When they subscribe to a group they receive push notifications over WhatsApp whenever a learning note is created in that group. A learning note is a very useful thing to learn about the topic of the group and can be created by a user of a group. The users can and are encouraged to enable their webcam and join. Per audio and video they can discuss which learning note should be created next. Because they are on video this also creates a community feel and members can encourage each others.

<h3>How did I build it?</h3>

The backend is build with ASP.NET Core. Entity Framework Core is used as an ORM and Azure SQL as the database. Blazor WebAssembly is used for the frontend. JavaScript is also in use in the frontend to make Twilio programmable video work. Twilio programmable video works on top of WebRTC and is needed to make the video livestream work. The application is hosted on Azure with Azure App Service. GitHub Actions are used for the CI/CD pipeline. Twilio programmable text is used to send the text messages.
