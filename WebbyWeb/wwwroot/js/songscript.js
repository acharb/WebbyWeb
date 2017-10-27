
const app={};

app.apiUrl = 'GET https://accounts.spotify.com/authorize/?client_id=&198f39964d94430298b55828752e20e0&response_type=code&redirect_uri=http://google.com';

app.events = function(){
    $('form').on('submit',function(e) {
    e.preventDefault();
    let artists = $('input[type=search]').val();    //$ selects html elements

    console.log(artists);
    let search = app.searchArtist(artists);


    /*artists.split(',');*/
    /*let search = artists.map(artistName => app.searchArtist(artistName));*/

    console.log(search);

    /*app.getAlbums(artists);

    /*
    $.when(...search)   /*... operator passes in array one value at a time, so after each element passed in, goes to .then method*
        .then((...artists)=>{
            artists = artists.map(a=>a[0].artists.items[0].id);
            console.log(artists);
            app.getAlbums(artists);
        });*/

    });

};

app.searchArtist = (artistName) => $.ajax({ /*ajax allows us to update web page without reloading page*/
    url:'${app.apiUrl}/search',
    method:'GET',                 
    dataType:'json',           
    data:{                      
        q:artistName,
        type:'artist'
    }
});

app.getAlbums = function(artists) {
    /*let albums = artists.map(artist => app.getAristsAlbums(artist.id));*/
    let albums = app.getAristsAlbums(artists.id);
    $.when(...albums)
        .then((...albums) => {
            let albumIds = albums
                .map(a => a[0].items)
                .reduce((prev,curr) => [...prev,...curr] ,[])
                .map(album => app.getAlbumTracks(album.id));

            app.getTracks(albumIds);
        });
};

app.getAristsAlbums = (id) => $.ajax({
    url: `https://api.spotify.com/v1/artists/${id}/albums`,
    method: 'GET',
    dataType: 'json',
    data: {
        album_type: 'album',
    }
});

app.getAlbumTracks = (id) => $.ajax({
    url: `https://api.spotify.com/v1/albums/${id}/tracks`,
    method: 'GET',
    dataType: 'json'
});

app.getTracks = function(tracks) {
    $.when(...tracks)
        .then((...tracks) => {
            tracks = tracks
                .map(getDataObject)
                .reduce((prev,curr) => [...prev,...curr],[]);   
            const randomPlayList = getRandomTracks(50,tracks);
            app.createPlayList(randomPlayList);
        })
};

app.createPlayList = function(songs) {
    const baseUrl = 'https://embed.spotify.com/?theme=white&uri=spotify:trackset:My Playlist:';
    songs = songs.map(song => song.id).join(',');
    $('.loader').removeClass('show');
    $('.playlist').append(`<iframe src="${baseUrl + songs}" height="400"></iframe>`);
}

/*get artist from spotify*/





app.init=function(){
    app.events();
};

$(app.init);
/*$(document).ready(app.init);*/