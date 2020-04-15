# School Project
The outcome of this assignment is to have a functional API for the organization "Sveriges Förenade Filmstudios" that should be used by a management team. The team should be able to use the API to rent a movie to a specific studio as well as be able to return it. Restrictions should also be set in the API to prevent a studio from borrowing the same movie if the maximum amount of rentals has been met. 

## Requirements 1.0
- API should be built in ASP.NET Core 3.1 or 3.1
- All endpoints should return JSON-data 
- API should consist of the following resuources and functionality
  -- Movies
    --- Add new movies
    --- Change maximum number of rentals of a movie. If the maximum value is reached, the movie should be unable to be rented.
    --- Set a movie to be rented by the studio 
    --- Set a movie to have been returned by the studio
  
