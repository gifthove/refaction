namespace refactor_me.data.Mapper
{
    using AutoMapper;
    using Models = refactor_me.core.Models;

    /// <summary>
    /// Class RepositoryMapper.
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class RepositoryMapper : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryMapper"/> class.
        /// </summary>
        public RepositoryMapper()
        {
            CreateMap<Models.Product, Product>().ReverseMap();
            CreateMap<Models.ProductOption, ProductOption>().ReverseMap(); 
        }
    }
}
