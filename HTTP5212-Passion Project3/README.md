# Artwork Sharing App CMS
This is a content management system for a web app for artist to showcase their artwork online. This web application used code-first migration to create databases, and can preform CRUD features. Please note, that the not all the features are complete. 

## Database 
There are 3 databases in this application, which are the Artist table, Artwork table, and Comments table. The Artist table has 3 columns (ArtistId, FirstName, LastName). The Artwork table has 5 columns (ArtworkId, ArtworkName, ArtworkDescription, ArtistId (Foreign Key), and ArtworkImage). The comment table has 3 columns (CommentId, CommentText, ArtworkId). 

## Using the Database and Application
### Getting data from the API:
Getting List of Artists https://localhost:44307/api/artistdata/listartists 
Getting List of Artworks https://localhost:44307/api/artworkdata/listartwork
Getting List of Comment https://localhost:44307/api/commentdata/listcomments 
### Running web app on browser:
View List of Artists https://localhost:44307/Artist/List
View List of Artworks https://localhost:44307/Artwork/List
View List of Comments https://localhost:44307/Comment/List 
Add a New Artist https://localhost:44307/Artist/New
Add a New Artwork https://localhost:44307/Artwork/New
Add a New Comment https://localhost:44307/Comment/New
### Using Command Line to Update and Delete:
Delete an artist in Command Line: curl -d "" https://localhost:44307/api/artistdata/deleteartist/{id}
Delete an artwork in Command Line:
curl -d "" https://localhost:44307/api/artistdata/deleteartwork/{id}
Delete a comment in Command Line: 
curl -d "" https://localhost:44307/api/artistdata/deletecomment/{id}
Update an artist on Command Line: curl -d @Artist.json -H "Content-type:application/json" localhost/api/ArtistData/UpdateArtist/{id}
Update an artwork on Command Line: curl -d @Artist.json -H "Content-type:application/json" localhost/api/ArtworkData/UpdateArtwork/{id}
Update an artist on Command Line: curl -d @Artist.json -H "Content-type:application/json" localhost/api/CommentData/UpdateComment/{id}

## Upcoming Features
Full CRUD capabilities running on the Views (including Update and Delete)
Ability to upload images and display them on web page. 

