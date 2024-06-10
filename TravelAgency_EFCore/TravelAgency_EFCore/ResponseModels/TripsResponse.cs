﻿namespace TravelAgency_EFCore.ResponseModels;

public class TripsResponse
{
    public int PageNum { get; set; }
    public int PageSize { get; set; }
    public int AllPages { get; set; }
    public List<TripDTO> Trips { get; set; }
}