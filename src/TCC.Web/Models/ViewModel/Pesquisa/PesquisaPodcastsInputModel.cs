using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TCC.Web.Models.ViewModel.Pesquisa
{
    public class PesquisaPodcastsInputModel
    {
        public string termo { get; set; }
    }
}
