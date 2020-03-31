﻿using System;
using System.Collections.Generic;
using System.Linq;
using Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Server.Controllers
{
    public class VolumeController: IVolumeController
    {
        private MaterialController materialController;
        private AddressController addressController;
        private readonly GTLContext _context;

        public VolumeController(GTLContext context)
        {
            materialController = new MaterialController();
            addressController = new AddressController();
            _context = context;

        }

        public Volume Create(int materialID, int homeLocationID, int currentLocationID)
        {
     
            try
            {
                var material = materialController.FindByID(materialID);
                var homeLocation = addressController.FindByID(homeLocationID);
                var currentLocation = addressController.FindByID(currentLocationID);

                var volume = new Volume { Material = material, CurrentLocation = currentLocation, HomeLocation = homeLocation };
                Insert(volume);
                return FindByID(ID: volume.VolumeId);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        
        }

        public int Delete(Volume t)
        {
            throw new NotImplementedException();
        }

        public List<Volume> FindAll()
        {
            IQueryable<Volume> volumes = _context.Volumes
                .Include(v => v.Material)
                    .ThenInclude(m => m.MaterialAuthors)
                        .ThenInclude(ma => ma.Author)
                .Include(v => v.Material)
                    .ThenInclude(m => m.MaterialSubjects)
                        .ThenInclude(ms => ms.MaterialSubject)
                .Include(v => v.HomeLocation)
                    .ThenInclude(a => a.Zip)
                .Include(v => v.CurrentLocation)
                    .ThenInclude(a => a.Zip);

            return volumes.ToList();
        }

        public List<Volume> FindVolumesForMaterial(int materialId)
        {
            try
            {
                IQueryable<Volume> volumes = _context.Volumes.Where(v => v.MaterialId == materialId)
              .Include(v => v.Material)
                  .ThenInclude(m => m.MaterialAuthors)
                      .ThenInclude(ma => ma.Author)
              .Include(v => v.Material)
                  .ThenInclude(m => m.MaterialSubjects)
                      .ThenInclude(ms => ms.MaterialSubject)
              .Include(v => v.HomeLocation)
                  .ThenInclude(a => a.Zip)
              .Include(v => v.CurrentLocation)
                  .ThenInclude(a => a.Zip);

                return volumes.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
        }

        public Volume FindByID(int ID)
        {
            try
            {
                var volume = _context.Volumes
        .Include(v => v.Material)
            .ThenInclude(m => m.MaterialAuthors)
                .ThenInclude(ma => ma.Author)
        .Include(v => v.Material)
            .ThenInclude(m => m.MaterialSubjects)
            .ThenInclude(ms => ms.MaterialSubject)
        .Include(v => v.HomeLocation)
            .ThenInclude(a => a.Zip)
         .Include(v => v.CurrentLocation)
             .ThenInclude(a => a.Zip)
         .Single(v => v.VolumeId == ID);

                return volume;
            }
            catch
            {
                throw new NullReferenceException(message: $"Volume with id: {ID} not found");
            }
    
        }

        public Volume FindByType(Volume t)
        {
            throw new NotImplementedException();
        }

        public Volume Insert(Volume volume)
        {
            _context.Volumes.Add(volume);
            _context.SaveChanges();
            return volume;
        }

        public Volume Update(Volume t)
        {
            throw new NotImplementedException();
        }
    }
}
