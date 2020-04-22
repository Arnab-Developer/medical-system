﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MedicalSystem.FrontEnds.WebMvc.Models
{
    public class ConsultationModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
        [JsonPropertyName("country")]
        public string? Country { get; set; }
        [JsonPropertyName("state")]
        public string? State { get; set; }
        [JsonPropertyName("city")]
        public string? City { get; set; }
        [JsonPropertyName("pinCode")]
        public string? PinCode { get; set; }
        [JsonPropertyName("problem")]
        public string? Problem { get; set; }
        [JsonPropertyName("medicine")]
        public string? Medicine { get; set; }
        [JsonPropertyName("doctorId")]
        public int DoctorId { get; set; }
        [JsonPropertyName("doctor")]
        public DoctorModel? Doctor { get; set; }
        [JsonPropertyName("patentId")]
        public int PatentId { get; set; }
        [JsonPropertyName("patent")]
        public PatentModel? Patent { get; set; }

        [JsonPropertyName("doctors")]
        public IEnumerable<DoctorModel>? Doctors { get; set; }
        [JsonPropertyName("patents")]
        public IEnumerable<PatentModel>? Patents { get; set; }

        public IEnumerable<SelectListItem>? DoctorSelectList { get; set; }
        public IEnumerable<SelectListItem>? PatentSelectList { get; set; }
    }
}
