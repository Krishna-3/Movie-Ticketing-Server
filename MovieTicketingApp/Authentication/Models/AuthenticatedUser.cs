﻿using MovieTicketingApp.DTO;

namespace MovieTicketingApp.Authentication.Models
{
    public class AuthenticatedUser
    {
        public string AccessToken{ get; set;}

        public string RefreshToken{ get; set;}
    }
}
