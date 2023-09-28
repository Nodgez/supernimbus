# Kevin Daly - Super Nimbus Junior Developer Assesment

## Steps:
- Run Server [Download](https://github.com/Nodgez/supernimbus/tree/master/SERVER%20BUILD)
- Run Client [Download](https://github.com/Nodgez/supernimbus/tree/master/Builds)
- Sign Up/Login
- Join Lobby
- Join Session
- W,S for player movement
- Score 3 to end the game

## Server:

The server for the game can be run headless. The repo contains a build of the server located [here](https://github.com/Nodgez/supernimbus/tree/master/SERVER%20BUILD). 
It is built using photon fusion and is authoritive over player's movement and score.
The server can also be run inside Unity with the build target set to Dedicated Server. 

When the server is launched it sets up a lobby and a session with a GUID. A session name can be provided in Unity before building if you want to test with a custom name.
All of the session are in the same lobby.

If a player diconnects from a match then the server kicks the remaining player from the session and waits for 2 new players to start up.

## Client

The client starts with a login page that is hooked up to lootlocker. The only feature this app has is user authentication.
Create a user or sign in as an exiting one -> Username : qwerty PWD: qwertyui

Once authenticated the user can then join the lobby. After the lobby is joined the user will see a list of session on the right to join
Once 2 players have joined a session the game will start until a player scores 3 points.
Controls are W, S for up and down and use the client input authority to notifiy the server of movement controls

The server then shutsdown the players and waits for new players to enter the session to restart the game.

## TODO:

- Improve the overall flow of the app with more in depth message handling
- Abstract the code away from the network runner objects more
- Lauch server from command line or a service with with a session and lobby name set via the player
- Implement a leaderboards table for players that win matches
- If one player leaves a session we should wait in that session for a new player instead of kicking the remaining player
