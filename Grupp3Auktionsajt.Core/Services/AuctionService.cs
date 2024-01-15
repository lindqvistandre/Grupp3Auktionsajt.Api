using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grupp3Auktionsajt.Data.Interfaces;


namespace Grupp3Auktionsajt.Core.Services
{
    public class AuctionService
    {
        private readonly IAuctionRepo _repo;