using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Core.Data;
using Nop.Plugin.Widgets.CSlider.Domain;

namespace Nop.Plugin.Widgets.CSlider.Services
{
    public class CSliderService : ICSliderService
    {
        private readonly IRepository<CSliderSlide> _repository;

        public CSliderService(IRepository<CSliderSlide> repository)
        {
            _repository = repository;
        }

        public void DeleteSlide(CSliderSlide slide)
        {
            if (slide == null)
                throw new ArgumentNullException("slide");

            _repository.Delete(slide);
        }

        public IList<CSliderSlide> GetAll()
        {
            var query = from gp in _repository.Table
                        orderby gp.Id
                        select gp;
            var records = query.ToList();
            return records;
        }

        public CSliderSlide GetById(int slideId)
        {
            if (slideId == 0)
                return null;

            return _repository.GetById(slideId);
        }

        public void InsertSlide(CSliderSlide slide)
        {
            if (slide == null)
                throw new ArgumentNullException("slide");

            _repository.Insert(slide);
        }

        public void UpdateSlide(CSliderSlide slide)
        {
            if (slide == null)
                throw new ArgumentNullException("slide");

            _repository.Update(slide);
        }

        public void InsertSlides(IEnumerable<CSliderSlide> slides)
        {
            if (slides == null)
                throw new ArgumentNullException("slides");

            foreach (var slide in slides)
            {
                InsertSlide(slide);
            }
        }
    }
}