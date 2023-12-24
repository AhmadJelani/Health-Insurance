using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Health_Insurance.Models;

public partial class HomePage
{
    public decimal Id { get; set; }
    [NotMapped]
    public IFormFile? Bg_ImageOne { get; set; }
    public string? BgImageOne { get; set; }
    [NotMapped]
    public IFormFile? Bg_ImageTwo { get; set; }
    public string? BgImageTwo { get; set; }
    [NotMapped]
    public IFormFile? Bg_ImageThree { get; set; }
    public string? BgImageThree { get; set; }
    [NotMapped]
    public IFormFile? Bg_ImageFour { get; set; }
    public string? BgImageFour { get; set; }
    [NotMapped]
    public IFormFile? Bg_ImageFive { get; set; }
    public string? BgImageFive { get; set; }

    public string? TitleCardOne { get; set; }

    public string? TextCardOne { get; set; }

    public string? TitleCardThree { get; set; }

    public string? TextCardThree { get; set; }

    public string? IntroTitle { get; set; }

    public string? IntroText { get; set; }

    public string? PointOneTitle { get; set; }

    public string? PointOneText { get; set; }

    public string? PointTwoTitle { get; set; }

    public string? PointTwoText { get; set; }

    public string? PointThreeTitle { get; set; }

    public string? PointThreeText { get; set; }

    public string? TitleEmer { get; set; }

    public string? TextEmer { get; set; }

    public string? TitleSubs { get; set; }

    public string? TextSubs { get; set; }

    public string? Feedback { get; set; }
}
