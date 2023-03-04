using ProVendas.Data.Repository;
using ProVendas.Domain.IRepository;

namespace ProVendas.API.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependences(this IServiceCollection services)
        {
            services.AddCors();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<ITipoPessoaRepository, TipoPessoaRepository>();
            services.AddScoped<ICidadeRepository, CidadeRepository>();
            services.AddScoped<IEstadoRepository, EstadoRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IFilialRepository, FilialRepository>();
            services.AddScoped<IPessoaRepository, PessoaRepository>();
            services.AddScoped<IPerfilRepository, PerfilRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            

            return services;
        }
    }
}
