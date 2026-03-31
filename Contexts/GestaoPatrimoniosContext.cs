using System;
using System.Collections.Generic;
using Gestao_Patrimonio.Domains;
using Microsoft.EntityFrameworkCore;

namespace Gestao_Patrimonio.Contexts;

public partial class GestaoPatrimoniosContext : DbContext
{
    public GestaoPatrimoniosContext()
    {
    }

    public GestaoPatrimoniosContext(DbContextOptions<GestaoPatrimoniosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Area> Area { get; set; }

    public virtual DbSet<Bairro> Bairro { get; set; }

    public virtual DbSet<Cargo> Cargo { get; set; }

    public virtual DbSet<Cidade> Cidade { get; set; }

    public virtual DbSet<Endereco> Endereco { get; set; }

    public virtual DbSet<Localizacao> Localizacao { get; set; }

    public virtual DbSet<LogPatrimonio> LogPatrimonio { get; set; }

    public virtual DbSet<Patrimonio> Patrimonio { get; set; }

    public virtual DbSet<SolictacaoTransferencia> SolictacaoTransferencia { get; set; }

    public virtual DbSet<StatusPatrimonio> StatusPatrimonio { get; set; }

    public virtual DbSet<StatusTransferencia> StatusTransferencia { get; set; }

    public virtual DbSet<TipoAlteracao> TipoAlteracao { get; set; }

    public virtual DbSet<TipoPatrimonio> TipoPatrimonio { get; set; }

    public virtual DbSet<TipoUsuario> TipoUsuario { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Area>(entity =>
        {
            entity.HasKey(e => e.AreaID).HasName("PK__Area__70B8202810B96C93");

            entity.HasIndex(e => e.NomeArea, "UQ__Area__9A7797600544EF39").IsUnique();

            entity.Property(e => e.AreaID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.NomeArea)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Bairro>(entity =>
        {
            entity.HasKey(e => e.BairroID).HasName("PK__Bairro__4A09362364321DD7");

            entity.Property(e => e.BairroID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CidadeID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.NomeBairro)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Cidade).WithMany(p => p.Bairro)
                .HasForeignKey(d => d.CidadeID)
                .HasConstraintName("FK_Bairro_Cidade");
        });

        modelBuilder.Entity<Cargo>(entity =>
        {
            entity.HasKey(e => e.CargoID).HasName("PK__Cargo__B4E665ED304035C1");

            entity.HasIndex(e => e.NomeCargo, "UQ__Cargo__4D9FD7DEF756E333").IsUnique();

            entity.Property(e => e.CargoID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.NomeCargo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Cidade>(entity =>
        {
            entity.HasKey(e => e.CidadeID).HasName("PK__Cidade__B68009598308DFF4");

            entity.Property(e => e.CidadeID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NomeCidade)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Endereco>(entity =>
        {
            entity.HasKey(e => e.EnderecoID).HasName("PK__Endereco__B9D9462FF60355DC");

            entity.Property(e => e.EnderecoID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CEP)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Complemento)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Logradouro)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Bairro).WithMany(p => p.Endereco)
                .HasForeignKey(d => d.BairroID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Endereco_Bairro");
        });

        modelBuilder.Entity<Localizacao>(entity =>
        {
            entity.HasKey(e => e.LocalizacaoID).HasName("PK__Localiza__83ABDECA225DAB8A");

            entity.ToTable(tb => tb.HasTrigger("trg_Local_SoftDelete"));

            entity.Property(e => e.LocalizacaoID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Ativo).HasDefaultValue(true);
            entity.Property(e => e.DescricaoSAP)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NomeLocal)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Area).WithMany(p => p.Localizacao)
                .HasForeignKey(d => d.AreaID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Localizacao_Area");

            entity.HasMany(d => d.Usuario).WithMany(p => p.Localizacao)
                .UsingEntity<Dictionary<string, object>>(
                    "LocalUsuario",
                    r => r.HasOne<Usuario>().WithMany()
                        .HasForeignKey("UsuarioID")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_LocalUsuario_Usuario"),
                    l => l.HasOne<Localizacao>().WithMany()
                        .HasForeignKey("LocalizacaoID")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_LocalUsuario_Localizacao"),
                    j =>
                    {
                        j.HasKey("LocalizacaoID", "UsuarioID");
                    });
        });

        modelBuilder.Entity<LogPatrimonio>(entity =>
        {
            entity.HasKey(e => e.LogPatrimonioID).HasName("PK__LogPatri__E716D12B98BD609C");

            entity.Property(e => e.LogPatrimonioID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.DataTransferencia).HasPrecision(0);

            entity.HasOne(d => d.Localizacao).WithMany(p => p.LogPatrimonio)
                .HasForeignKey(d => d.LocalizacaoID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LogPatrimonio_Localizacao");

            entity.HasOne(d => d.Patrimonio).WithMany(p => p.LogPatrimonio)
                .HasForeignKey(d => d.PatrimonioID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LogPatrimonio_Patrimonio");

            entity.HasOne(d => d.StatusPatrimonio).WithMany(p => p.LogPatrimonio)
                .HasForeignKey(d => d.StatusPatrimonioID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LogPatrimonio_StatusPatrimonio");

            entity.HasOne(d => d.TipoAlteracao).WithMany(p => p.LogPatrimonio)
                .HasForeignKey(d => d.TipoAlteracaoID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LogPatrimonio_TipoAlteracao");

            entity.HasOne(d => d.Usuario).WithMany(p => p.LogPatrimonio)
                .HasForeignKey(d => d.UsuarioID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LogPatrimonio_Usuario");
        });

        modelBuilder.Entity<Patrimonio>(entity =>
        {
            entity.HasKey(e => e.PatrimonioID).HasName("PK__Patrimon__C5A60BDEACF371F1");

            entity.ToTable(tb => tb.HasTrigger("trg_Patrimonio_SoftDelete"));

            entity.Property(e => e.PatrimonioID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Denominacao).IsUnicode(false);
            entity.Property(e => e.Imagem).IsUnicode(false);
            entity.Property(e => e.NumerPatrimonio)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Localizacao).WithMany(p => p.Patrimonio)
                .HasForeignKey(d => d.LocalizacaoID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Patrimonio_Localizacao");

            entity.HasOne(d => d.StatusPatrimonio).WithMany(p => p.Patrimonio)
                .HasForeignKey(d => d.StatusPatrimonioID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Patrimonio_StatusPatrimonio");

            entity.HasOne(d => d.TipoPatimonio).WithMany(p => p.Patrimonio)
                .HasForeignKey(d => d.TipoPatimonioID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Patrimonio_TipoPatrimonio");
        });

        modelBuilder.Entity<SolictacaoTransferencia>(entity =>
        {
            entity.HasKey(e => e.TransferenciaID).HasName("PK__Solictac__E5B4F5F205DF01D5");

            entity.Property(e => e.TransferenciaID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.DataCriacaoSolicitante).HasPrecision(0);
            entity.Property(e => e.DataResposta).HasPrecision(0);
            entity.Property(e => e.Justificativa).IsUnicode(false);

            entity.HasOne(d => d.Localizacao).WithMany(p => p.SolictacaoTransferencia)
                .HasForeignKey(d => d.LocalizacaoID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SolicitacaoTranferencia_Localizacao");

            entity.HasOne(d => d.Patrimonio).WithMany(p => p.SolictacaoTransferencia)
                .HasForeignKey(d => d.PatrimonioID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SolicitacaoTranferencia_Patrimonio");

            entity.HasOne(d => d.StatusTransferencia).WithMany(p => p.SolictacaoTransferencia)
                .HasForeignKey(d => d.StatusTransferenciaID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SolicitacaoTranferencia_StatusTransferencia");

            entity.HasOne(d => d.UsuarioIDAprovacaoNavigation).WithMany(p => p.SolictacaoTransferenciaUsuarioIDAprovacaoNavigation)
                .HasForeignKey(d => d.UsuarioIDAprovacao)
                .HasConstraintName("FK_SolicitacaoTranferencia_UsuarioAprovacao");

            entity.HasOne(d => d.UsuarioIDSolicitacaoNavigation).WithMany(p => p.SolictacaoTransferenciaUsuarioIDSolicitacaoNavigation)
                .HasForeignKey(d => d.UsuarioIDSolicitacao)
                .HasConstraintName("FK_SolicitacaoTranferencia_UsuarioSolicitacao");
        });

        modelBuilder.Entity<StatusPatrimonio>(entity =>
        {
            entity.HasKey(e => e.StatusPatrimonioID).HasName("PK__StatusPa__B3F33609D65CBD9A");

            entity.HasIndex(e => e.NomeStatus, "UQ__StatusPa__C5C60F1A8495036F").IsUnique();

            entity.Property(e => e.StatusPatrimonioID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.NomeStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<StatusTransferencia>(entity =>
        {
            entity.HasKey(e => e.StatusTransferenciaID).HasName("PK__StatusTr__7AA828B9FFE181BF");

            entity.HasIndex(e => e.NomeStatus, "UQ__StatusTr__C5C60F1AE0D99EA2").IsUnique();

            entity.Property(e => e.StatusTransferenciaID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.NomeStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoAlteracao>(entity =>
        {
            entity.HasKey(e => e.TipoAlteracaoID).HasName("PK__TipoAlte__9BEF4F0DEDDB847E");

            entity.Property(e => e.TipoAlteracaoID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.NomeTipo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoPatrimonio>(entity =>
        {
            entity.HasKey(e => e.TipoPatrimonioID).HasName("PK__TipoPatr__4DC9FF9969185CE2");

            entity.HasIndex(e => e.NomeTipo, "UQ__TipoPatr__7859A10A634A438D").IsUnique();

            entity.Property(e => e.TipoPatrimonioID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.NomeTipo)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoUsuario>(entity =>
        {
            entity.HasKey(e => e.TipoUsuarioID).HasName("PK__TipoUsua__7F22C702F8C5E975");

            entity.HasIndex(e => e.NomeTipo, "UQ__TipoUsua__7859A10AC26CEC67").IsUnique();

            entity.Property(e => e.TipoUsuarioID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.NomeTipo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioID).HasName("PK__Usuario__2B3DE798ACE60F4A");

            entity.ToTable(tb => tb.HasTrigger("trg_Usuario_SoftDelete"));

            entity.HasIndex(e => e.RG, "UQ__Usuario__321537C85F839BCE").IsUnique();

            entity.HasIndex(e => e.CarteiraTrabalho, "UQ__Usuario__6E25BCA2E08D5197").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Usuario__A9D10534A72CCD38").IsUnique();

            entity.HasIndex(e => e.CPF, "UQ__Usuario__C1F89731CF17BE6B").IsUnique();

            entity.HasIndex(e => e.NIF, "UQ__Usuario__C7DEC33080FD3A73").IsUnique();

            entity.Property(e => e.UsuarioID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Ativo).HasDefaultValue(true);
            entity.Property(e => e.CPF)
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.CarteiraTrabalho)
                .HasMaxLength(14)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.NIF)
                .HasMaxLength(7)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.PrimeiroAcesso).HasDefaultValue(true);
            entity.Property(e => e.RG)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Senha).HasMaxLength(32);

            entity.HasOne(d => d.Cargo).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.CargoID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Cargo");

            entity.HasOne(d => d.Endereco).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.EnderecoID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Endereco");

            entity.HasOne(d => d.TipoUsuario).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.TipoUsuarioID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_TipoUsuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
