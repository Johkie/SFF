var URL_MoviesAPI = "";
var URL_StudiosAPI = "";

async function getDataAsync(url)
{
    let response = await fetch(url);
    let data = await response.json();
    return data;
}

function buildMovieList()
{
    getDataAsync(URL_HighscoreAPI).then(data => {
        data.forEach(element => {
            // Create highscoreitem
            var movieRow = document.createElement("tr");
            movieRow.className = "movie-tr"
            movieRow.id = movieRow.className + element.id;
 
            movieRow.insertCell(0).innerHTML = element.id;
            movieRow.insertCell(1).innerHTML = element.movie;
            document.getElementById("moviesTable").appendChild(movieRow);
        });
    });
}

function buildStudios()
{
    getDataAsync(URL_UserSettingsAPI).then(data => {
        data.forEach(element => {
            // Create highscoreitem
            var studioRow = document.createElement("tr");
            studioRow.className = "studio-tr"
            studioRow.id = movieRow.className + element.id;
 
            studioRow.insertCell(0).innerHTML = element.id;
            studioRow.insertCell(1).innerHTML = element.studio;
            document.getElementById("studioTable").appendChild(studioRow);
        });
    })
}

function buildSite()
{
    buildMovieList()
    buildStudios();
}
