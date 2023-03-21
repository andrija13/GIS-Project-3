using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GISWeb.Models;

public partial class GisdbContext : DbContext
{
    public GisdbContext()
    {
    }

    public GisdbContext(DbContextOptions<GisdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<NisexportLearngisShpLine> NisexportLearngisShpLines { get; set; }

    public virtual DbSet<NisexportLearngisShpPoint> NisexportLearngisShpPoints { get; set; }

    public virtual DbSet<NisexportLearngisShpPoly> NisexportLearngisShpPolies { get; set; }

    public virtual DbSet<PlanetOsmLine> PlanetOsmLines { get; set; }

    public virtual DbSet<PlanetOsmPoint> PlanetOsmPoints { get; set; }

    public virtual DbSet<PlanetOsmPolygon> PlanetOsmPolygons { get; set; }

    public virtual DbSet<PlanetOsmRoad> PlanetOsmRoads { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=GISDB;Username=postgres;Password=admin");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("postgis");

        modelBuilder.Entity<NisexportLearngisShpLine>(entity =>
        {
            entity.HasKey(e => e.Gid).HasName("nisexport--learngis_230cab89-008e-48b0-b740-b032ab2a0656_s_pkey");

            entity.ToTable("nisexport-learngis_shp_line");

            entity.Property(e => e.Gid)
                .HasDefaultValueSql("nextval('\"nisexport--learngis_230cab89-008e-48b0-b740-b032ab2a065_gid_seq\"'::regclass)")
                .HasColumnName("gid");
            entity.Property(e => e.Aeroway)
                .HasMaxLength(80)
                .HasColumnName("aeroway");
            entity.Property(e => e.Amenity)
                .HasMaxLength(80)
                .HasColumnName("amenity");
            entity.Property(e => e.Barrier)
                .HasMaxLength(80)
                .HasColumnName("barrier");
            entity.Property(e => e.Blockage)
                .HasMaxLength(80)
                .HasColumnName("blockage");
            entity.Property(e => e.Bridge)
                .HasMaxLength(80)
                .HasColumnName("bridge");
            entity.Property(e => e.Building)
                .HasMaxLength(80)
                .HasColumnName("building");
            entity.Property(e => e.Capacity)
                .HasMaxLength(80)
                .HasColumnName("capacity");
            entity.Property(e => e.Covered)
                .HasMaxLength(80)
                .HasColumnName("covered");
            entity.Property(e => e.Depth)
                .HasMaxLength(80)
                .HasColumnName("depth");
            entity.Property(e => e.Diameter)
                .HasMaxLength(80)
                .HasColumnName("diameter");
            entity.Property(e => e.Highway)
                .HasMaxLength(80)
                .HasColumnName("highway");
            entity.Property(e => e.Industrial)
                .HasMaxLength(80)
                .HasColumnName("industrial");
            entity.Property(e => e.Landuse)
                .HasMaxLength(80)
                .HasColumnName("landuse");
            entity.Property(e => e.Layer)
                .HasMaxLength(80)
                .HasColumnName("layer");
            entity.Property(e => e.ManMade)
                .HasMaxLength(80)
                .HasColumnName("man_made");
            entity.Property(e => e.Name)
                .HasMaxLength(80)
                .HasColumnName("name");
            entity.Property(e => e.NameEn)
                .HasMaxLength(80)
                .HasColumnName("name_en");
            entity.Property(e => e.NameFr)
                .HasMaxLength(80)
                .HasColumnName("name_fr");
            entity.Property(e => e.NameSw)
                .HasMaxLength(80)
                .HasColumnName("name_sw");
            entity.Property(e => e.Natural)
                .HasMaxLength(80)
                .HasColumnName("natural");
            entity.Property(e => e.Oneway)
                .HasMaxLength(80)
                .HasColumnName("oneway");
            entity.Property(e => e.Operator)
                .HasMaxLength(80)
                .HasColumnName("operator");
            entity.Property(e => e.OsmId).HasColumnName("osm_id");
            entity.Property(e => e.Parking)
                .HasMaxLength(80)
                .HasColumnName("parking");
            entity.Property(e => e.PublicTra)
                .HasMaxLength(80)
                .HasColumnName("public_tra");
            entity.Property(e => e.Pump)
                .HasMaxLength(80)
                .HasColumnName("pump");
            entity.Property(e => e.Railway)
                .HasMaxLength(80)
                .HasColumnName("railway");
            entity.Property(e => e.Smoothness)
                .HasMaxLength(80)
                .HasColumnName("smoothness");
            entity.Property(e => e.SocialFac)
                .HasMaxLength(80)
                .HasColumnName("social_fac");
            entity.Property(e => e.Surface)
                .HasMaxLength(80)
                .HasColumnName("surface");
            entity.Property(e => e.Tunnel)
                .HasMaxLength(80)
                .HasColumnName("tunnel");
            entity.Property(e => e.Water)
                .HasMaxLength(80)
                .HasColumnName("water");
            entity.Property(e => e.Waterway)
                .HasMaxLength(80)
                .HasColumnName("waterway");
            entity.Property(e => e.Wheelchair)
                .HasMaxLength(80)
                .HasColumnName("wheelchair");
            entity.Property(e => e.Width)
                .HasMaxLength(80)
                .HasColumnName("width");
        });

        modelBuilder.Entity<NisexportLearngisShpPoint>(entity =>
        {
            entity.HasKey(e => e.Gid).HasName("nisexport--learngis_230cab89-008e-48b0-b740-b032ab2a0656__pkey1");

            entity.ToTable("nisexport-learngis_shp_point");

            entity.Property(e => e.Gid)
                .HasDefaultValueSql("nextval('\"nisexport--learngis_230cab89-008e-48b0-b740-b032ab2a06_gid_seq1\"'::regclass)")
                .HasColumnName("gid");
            entity.Property(e => e.Access)
                .HasMaxLength(80)
                .HasColumnName("access");
            entity.Property(e => e.AccessRoo)
                .HasMaxLength(80)
                .HasColumnName("access_roo");
            entity.Property(e => e.AddrHouse)
                .HasMaxLength(80)
                .HasColumnName("addr_house");
            entity.Property(e => e.AddrPostc)
                .HasMaxLength(80)
                .HasColumnName("addr_postc");
            entity.Property(e => e.AddrStree)
                .HasMaxLength(80)
                .HasColumnName("addr_stree");
            entity.Property(e => e.AdminLeve)
                .HasMaxLength(80)
                .HasColumnName("admin_leve");
            entity.Property(e => e.Aeroway)
                .HasMaxLength(80)
                .HasColumnName("aeroway");
            entity.Property(e => e.Amenity)
                .HasMaxLength(80)
                .HasColumnName("amenity");
            entity.Property(e => e.BackupGen)
                .HasMaxLength(80)
                .HasColumnName("backup_gen");
            entity.Property(e => e.Barrier)
                .HasMaxLength(80)
                .HasColumnName("barrier");
            entity.Property(e => e.Beds)
                .HasMaxLength(80)
                .HasColumnName("beds");
            entity.Property(e => e.Blockage)
                .HasMaxLength(80)
                .HasColumnName("blockage");
            entity.Property(e => e.Boundary)
                .HasMaxLength(80)
                .HasColumnName("boundary");
            entity.Property(e => e.Bridge)
                .HasMaxLength(80)
                .HasColumnName("bridge");
            entity.Property(e => e.Building)
                .HasMaxLength(80)
                .HasColumnName("building");
            entity.Property(e => e.BuildingM)
                .HasMaxLength(80)
                .HasColumnName("building_m");
            entity.Property(e => e.Capacity)
                .HasMaxLength(80)
                .HasColumnName("capacity");
            entity.Property(e => e.Communic1)
                .HasMaxLength(80)
                .HasColumnName("communic_1");
            entity.Property(e => e.Communicat)
                .HasMaxLength(80)
                .HasColumnName("communicat");
            entity.Property(e => e.Covered)
                .HasMaxLength(80)
                .HasColumnName("covered");
            entity.Property(e => e.Denominati)
                .HasMaxLength(80)
                .HasColumnName("denominati");
            entity.Property(e => e.Depth)
                .HasMaxLength(80)
                .HasColumnName("depth");
            entity.Property(e => e.Diameter)
                .HasMaxLength(80)
                .HasColumnName("diameter");
            entity.Property(e => e.Emergency)
                .HasMaxLength(80)
                .HasColumnName("emergency");
            entity.Property(e => e.Fuel)
                .HasMaxLength(80)
                .HasColumnName("fuel");
            entity.Property(e => e.Government)
                .HasMaxLength(80)
                .HasColumnName("government");
            entity.Property(e => e.HealthF1)
                .HasMaxLength(80)
                .HasColumnName("health_f_1");
            entity.Property(e => e.HealthF2)
                .HasMaxLength(80)
                .HasColumnName("health_f_2");
            entity.Property(e => e.HealthFac)
                .HasMaxLength(80)
                .HasColumnName("health_fac");
            entity.Property(e => e.Healthcare)
                .HasMaxLength(80)
                .HasColumnName("healthcare");
            entity.Property(e => e.Highway)
                .HasMaxLength(80)
                .HasColumnName("highway");
            entity.Property(e => e.Historic)
                .HasMaxLength(80)
                .HasColumnName("historic");
            entity.Property(e => e.Industrial)
                .HasMaxLength(80)
                .HasColumnName("industrial");
            entity.Property(e => e.IsIn)
                .HasMaxLength(80)
                .HasColumnName("is_in");
            entity.Property(e => e.IscedLeve)
                .HasMaxLength(80)
                .HasColumnName("isced_leve");
            entity.Property(e => e.Landuse)
                .HasMaxLength(80)
                .HasColumnName("landuse");
            entity.Property(e => e.Layer)
                .HasMaxLength(80)
                .HasColumnName("layer");
            entity.Property(e => e.Leisure)
                .HasMaxLength(80)
                .HasColumnName("leisure");
            entity.Property(e => e.ManMade)
                .HasMaxLength(80)
                .HasColumnName("man_made");
            entity.Property(e => e.MedicalSy)
                .HasMaxLength(80)
                .HasColumnName("medical_sy");
            entity.Property(e => e.Military)
                .HasMaxLength(80)
                .HasColumnName("military");
            entity.Property(e => e.Name)
                .HasMaxLength(80)
                .HasColumnName("name");
            entity.Property(e => e.NameEn)
                .HasMaxLength(80)
                .HasColumnName("name_en");
            entity.Property(e => e.NameFr)
                .HasMaxLength(80)
                .HasColumnName("name_fr");
            entity.Property(e => e.NameSw)
                .HasMaxLength(80)
                .HasColumnName("name_sw");
            entity.Property(e => e.Natural)
                .HasMaxLength(80)
                .HasColumnName("natural");
            entity.Property(e => e.Network)
                .HasMaxLength(80)
                .HasColumnName("network");
            entity.Property(e => e.Office)
                .HasMaxLength(80)
                .HasColumnName("office");
            entity.Property(e => e.Oneway)
                .HasMaxLength(80)
                .HasColumnName("oneway");
            entity.Property(e => e.OpeningHo)
                .HasMaxLength(80)
                .HasColumnName("opening_ho");
            entity.Property(e => e.Operator)
                .HasMaxLength(106)
                .HasColumnName("operator");
            entity.Property(e => e.OperatorT)
                .HasMaxLength(80)
                .HasColumnName("operator_t");
            entity.Property(e => e.OsmId).HasColumnName("osm_id");
            entity.Property(e => e.Parking)
                .HasMaxLength(80)
                .HasColumnName("parking");
            entity.Property(e => e.Place)
                .HasMaxLength(80)
                .HasColumnName("place");
            entity.Property(e => e.Population)
                .HasMaxLength(80)
                .HasColumnName("population");
            entity.Property(e => e.Power)
                .HasMaxLength(80)
                .HasColumnName("power");
            entity.Property(e => e.PublicTra)
                .HasMaxLength(80)
                .HasColumnName("public_tra");
            entity.Property(e => e.Pump)
                .HasMaxLength(80)
                .HasColumnName("pump");
            entity.Property(e => e.Railway)
                .HasMaxLength(80)
                .HasColumnName("railway");
            entity.Property(e => e.Religion)
                .HasMaxLength(80)
                .HasColumnName("religion");
            entity.Property(e => e.RoofMater)
                .HasMaxLength(80)
                .HasColumnName("roof_mater");
            entity.Property(e => e.Rooms)
                .HasMaxLength(80)
                .HasColumnName("rooms");
            entity.Property(e => e.Shop)
                .HasMaxLength(80)
                .HasColumnName("shop");
            entity.Property(e => e.Smoothness)
                .HasMaxLength(80)
                .HasColumnName("smoothness");
            entity.Property(e => e.SocialFac)
                .HasMaxLength(80)
                .HasColumnName("social_fac");
            entity.Property(e => e.StaffCo1)
                .HasMaxLength(80)
                .HasColumnName("staff_co_1");
            entity.Property(e => e.StaffCoun)
                .HasMaxLength(80)
                .HasColumnName("staff_coun");
            entity.Property(e => e.Status)
                .HasMaxLength(80)
                .HasColumnName("status");
            entity.Property(e => e.Surface)
                .HasMaxLength(80)
                .HasColumnName("surface");
            entity.Property(e => e.ToiletsDi)
                .HasMaxLength(80)
                .HasColumnName("toilets_di");
            entity.Property(e => e.ToiletsHa)
                .HasMaxLength(80)
                .HasColumnName("toilets_ha");
            entity.Property(e => e.Tourism)
                .HasMaxLength(80)
                .HasColumnName("tourism");
            entity.Property(e => e.Tower)
                .HasMaxLength(80)
                .HasColumnName("tower");
            entity.Property(e => e.Tunnel)
                .HasMaxLength(80)
                .HasColumnName("tunnel");
            entity.Property(e => e.Water)
                .HasMaxLength(80)
                .HasColumnName("water");
            entity.Property(e => e.Waterway)
                .HasMaxLength(80)
                .HasColumnName("waterway");
            entity.Property(e => e.Wheelchair)
                .HasMaxLength(80)
                .HasColumnName("wheelchair");
            entity.Property(e => e.Width)
                .HasMaxLength(80)
                .HasColumnName("width");
        });

        modelBuilder.Entity<NisexportLearngisShpPoly>(entity =>
        {
            entity.HasKey(e => e.Gid).HasName("nisexport--learngis_230cab89-008e-48b0-b740-b032ab2a0656__pkey2");

            entity.ToTable("nisexport-learngis_shp_poly");

            entity.Property(e => e.Gid)
                .HasDefaultValueSql("nextval('\"nisexport--learngis_230cab89-008e-48b0-b740-b032ab2a06_gid_seq2\"'::regclass)")
                .HasColumnName("gid");
            entity.Property(e => e.Access)
                .HasMaxLength(80)
                .HasColumnName("access");
            entity.Property(e => e.AccessRoo)
                .HasMaxLength(80)
                .HasColumnName("access_roo");
            entity.Property(e => e.AddrHouse)
                .HasMaxLength(80)
                .HasColumnName("addr_house");
            entity.Property(e => e.AddrPostc)
                .HasMaxLength(80)
                .HasColumnName("addr_postc");
            entity.Property(e => e.AddrStree)
                .HasMaxLength(80)
                .HasColumnName("addr_stree");
            entity.Property(e => e.AdminLeve)
                .HasMaxLength(80)
                .HasColumnName("admin_leve");
            entity.Property(e => e.Aeroway)
                .HasMaxLength(80)
                .HasColumnName("aeroway");
            entity.Property(e => e.Amenity)
                .HasMaxLength(80)
                .HasColumnName("amenity");
            entity.Property(e => e.BackupGen)
                .HasMaxLength(80)
                .HasColumnName("backup_gen");
            entity.Property(e => e.Barrier)
                .HasMaxLength(80)
                .HasColumnName("barrier");
            entity.Property(e => e.Beds)
                .HasMaxLength(80)
                .HasColumnName("beds");
            entity.Property(e => e.Blockage)
                .HasMaxLength(80)
                .HasColumnName("blockage");
            entity.Property(e => e.Boundary)
                .HasMaxLength(80)
                .HasColumnName("boundary");
            entity.Property(e => e.Bridge)
                .HasMaxLength(80)
                .HasColumnName("bridge");
            entity.Property(e => e.Building)
                .HasMaxLength(80)
                .HasColumnName("building");
            entity.Property(e => e.BuildingM)
                .HasMaxLength(80)
                .HasColumnName("building_m");
            entity.Property(e => e.Capacity)
                .HasMaxLength(80)
                .HasColumnName("capacity");
            entity.Property(e => e.Communic1)
                .HasMaxLength(80)
                .HasColumnName("communic_1");
            entity.Property(e => e.Communicat)
                .HasMaxLength(80)
                .HasColumnName("communicat");
            entity.Property(e => e.Covered)
                .HasMaxLength(80)
                .HasColumnName("covered");
            entity.Property(e => e.Denominati)
                .HasMaxLength(80)
                .HasColumnName("denominati");
            entity.Property(e => e.Depth)
                .HasMaxLength(80)
                .HasColumnName("depth");
            entity.Property(e => e.Diameter)
                .HasMaxLength(80)
                .HasColumnName("diameter");
            entity.Property(e => e.Emergency)
                .HasMaxLength(80)
                .HasColumnName("emergency");
            entity.Property(e => e.Fuel)
                .HasMaxLength(80)
                .HasColumnName("fuel");
            entity.Property(e => e.Government)
                .HasMaxLength(80)
                .HasColumnName("government");
            entity.Property(e => e.HealthF1)
                .HasMaxLength(80)
                .HasColumnName("health_f_1");
            entity.Property(e => e.HealthF2)
                .HasMaxLength(80)
                .HasColumnName("health_f_2");
            entity.Property(e => e.HealthFac)
                .HasMaxLength(80)
                .HasColumnName("health_fac");
            entity.Property(e => e.Healthcare)
                .HasMaxLength(80)
                .HasColumnName("healthcare");
            entity.Property(e => e.Highway)
                .HasMaxLength(80)
                .HasColumnName("highway");
            entity.Property(e => e.Historic)
                .HasMaxLength(80)
                .HasColumnName("historic");
            entity.Property(e => e.Industrial)
                .HasMaxLength(80)
                .HasColumnName("industrial");
            entity.Property(e => e.IsIn)
                .HasMaxLength(80)
                .HasColumnName("is_in");
            entity.Property(e => e.IscedLeve)
                .HasMaxLength(80)
                .HasColumnName("isced_leve");
            entity.Property(e => e.Landuse)
                .HasMaxLength(80)
                .HasColumnName("landuse");
            entity.Property(e => e.Layer)
                .HasMaxLength(80)
                .HasColumnName("layer");
            entity.Property(e => e.Leisure)
                .HasMaxLength(80)
                .HasColumnName("leisure");
            entity.Property(e => e.ManMade)
                .HasMaxLength(80)
                .HasColumnName("man_made");
            entity.Property(e => e.MedicalSy)
                .HasMaxLength(80)
                .HasColumnName("medical_sy");
            entity.Property(e => e.Military)
                .HasMaxLength(80)
                .HasColumnName("military");
            entity.Property(e => e.Name)
                .HasMaxLength(80)
                .HasColumnName("name");
            entity.Property(e => e.NameEn)
                .HasMaxLength(80)
                .HasColumnName("name_en");
            entity.Property(e => e.NameFr)
                .HasMaxLength(80)
                .HasColumnName("name_fr");
            entity.Property(e => e.NameSw)
                .HasMaxLength(80)
                .HasColumnName("name_sw");
            entity.Property(e => e.Natural)
                .HasMaxLength(80)
                .HasColumnName("natural");
            entity.Property(e => e.Network)
                .HasMaxLength(80)
                .HasColumnName("network");
            entity.Property(e => e.Office)
                .HasMaxLength(80)
                .HasColumnName("office");
            entity.Property(e => e.Oneway)
                .HasMaxLength(80)
                .HasColumnName("oneway");
            entity.Property(e => e.OpeningHo)
                .HasMaxLength(80)
                .HasColumnName("opening_ho");
            entity.Property(e => e.Operator)
                .HasMaxLength(80)
                .HasColumnName("operator");
            entity.Property(e => e.OperatorT)
                .HasMaxLength(80)
                .HasColumnName("operator_t");
            entity.Property(e => e.OsmId).HasColumnName("osm_id");
            entity.Property(e => e.Parking)
                .HasMaxLength(80)
                .HasColumnName("parking");
            entity.Property(e => e.Place)
                .HasMaxLength(80)
                .HasColumnName("place");
            entity.Property(e => e.Population)
                .HasMaxLength(80)
                .HasColumnName("population");
            entity.Property(e => e.Power)
                .HasMaxLength(80)
                .HasColumnName("power");
            entity.Property(e => e.PublicTra)
                .HasMaxLength(80)
                .HasColumnName("public_tra");
            entity.Property(e => e.Pump)
                .HasMaxLength(80)
                .HasColumnName("pump");
            entity.Property(e => e.Railway)
                .HasMaxLength(80)
                .HasColumnName("railway");
            entity.Property(e => e.Religion)
                .HasMaxLength(80)
                .HasColumnName("religion");
            entity.Property(e => e.RoofMater)
                .HasMaxLength(80)
                .HasColumnName("roof_mater");
            entity.Property(e => e.Rooms)
                .HasMaxLength(80)
                .HasColumnName("rooms");
            entity.Property(e => e.Shop)
                .HasMaxLength(80)
                .HasColumnName("shop");
            entity.Property(e => e.Smoothness)
                .HasMaxLength(80)
                .HasColumnName("smoothness");
            entity.Property(e => e.SocialFac)
                .HasMaxLength(80)
                .HasColumnName("social_fac");
            entity.Property(e => e.StaffCo1)
                .HasMaxLength(80)
                .HasColumnName("staff_co_1");
            entity.Property(e => e.StaffCoun)
                .HasMaxLength(80)
                .HasColumnName("staff_coun");
            entity.Property(e => e.Status)
                .HasMaxLength(80)
                .HasColumnName("status");
            entity.Property(e => e.Surface)
                .HasMaxLength(80)
                .HasColumnName("surface");
            entity.Property(e => e.ToiletsDi)
                .HasMaxLength(80)
                .HasColumnName("toilets_di");
            entity.Property(e => e.ToiletsHa)
                .HasMaxLength(80)
                .HasColumnName("toilets_ha");
            entity.Property(e => e.Tourism)
                .HasMaxLength(80)
                .HasColumnName("tourism");
            entity.Property(e => e.Tower)
                .HasMaxLength(80)
                .HasColumnName("tower");
            entity.Property(e => e.Tunnel)
                .HasMaxLength(80)
                .HasColumnName("tunnel");
            entity.Property(e => e.Water)
                .HasMaxLength(80)
                .HasColumnName("water");
            entity.Property(e => e.Waterway)
                .HasMaxLength(80)
                .HasColumnName("waterway");
            entity.Property(e => e.Wheelchair)
                .HasMaxLength(80)
                .HasColumnName("wheelchair");
            entity.Property(e => e.Width)
                .HasMaxLength(80)
                .HasColumnName("width");
        });

        modelBuilder.Entity<PlanetOsmLine>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("planet_osm_line");

            entity.Property(e => e.Access).HasColumnName("access");
            entity.Property(e => e.AddrHousename).HasColumnName("addr:housename");
            entity.Property(e => e.AddrHousenumber).HasColumnName("addr:housenumber");
            entity.Property(e => e.AddrInterpolation).HasColumnName("addr:interpolation");
            entity.Property(e => e.AdminLevel).HasColumnName("admin_level");
            entity.Property(e => e.Aerialway).HasColumnName("aerialway");
            entity.Property(e => e.Aeroway).HasColumnName("aeroway");
            entity.Property(e => e.Amenity).HasColumnName("amenity");
            entity.Property(e => e.Area).HasColumnName("area");
            entity.Property(e => e.Barrier).HasColumnName("barrier");
            entity.Property(e => e.Bicycle).HasColumnName("bicycle");
            entity.Property(e => e.Boundary).HasColumnName("boundary");
            entity.Property(e => e.Brand).HasColumnName("brand");
            entity.Property(e => e.Bridge).HasColumnName("bridge");
            entity.Property(e => e.Building).HasColumnName("building");
            entity.Property(e => e.Construction).HasColumnName("construction");
            entity.Property(e => e.Covered).HasColumnName("covered");
            entity.Property(e => e.Culvert).HasColumnName("culvert");
            entity.Property(e => e.Cutting).HasColumnName("cutting");
            entity.Property(e => e.Denomination).HasColumnName("denomination");
            entity.Property(e => e.Disused).HasColumnName("disused");
            entity.Property(e => e.Embankment).HasColumnName("embankment");
            entity.Property(e => e.Foot).HasColumnName("foot");
            entity.Property(e => e.GeneratorSource).HasColumnName("generator:source");
            entity.Property(e => e.Harbour).HasColumnName("harbour");
            entity.Property(e => e.Highway).HasColumnName("highway");
            entity.Property(e => e.Historic).HasColumnName("historic");
            entity.Property(e => e.Horse).HasColumnName("horse");
            entity.Property(e => e.Intermittent).HasColumnName("intermittent");
            entity.Property(e => e.Junction).HasColumnName("junction");
            entity.Property(e => e.Landuse).HasColumnName("landuse");
            entity.Property(e => e.Layer).HasColumnName("layer");
            entity.Property(e => e.Leisure).HasColumnName("leisure");
            entity.Property(e => e.Lock).HasColumnName("lock");
            entity.Property(e => e.ManMade).HasColumnName("man_made");
            entity.Property(e => e.Military).HasColumnName("military");
            entity.Property(e => e.Motorcar).HasColumnName("motorcar");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Natural).HasColumnName("natural");
            entity.Property(e => e.Office).HasColumnName("office");
            entity.Property(e => e.Oneway).HasColumnName("oneway");
            entity.Property(e => e.Operator).HasColumnName("operator");
            entity.Property(e => e.OsmId).HasColumnName("osm_id");
            entity.Property(e => e.Place).HasColumnName("place");
            entity.Property(e => e.Population).HasColumnName("population");
            entity.Property(e => e.Power).HasColumnName("power");
            entity.Property(e => e.PowerSource).HasColumnName("power_source");
            entity.Property(e => e.PublicTransport).HasColumnName("public_transport");
            entity.Property(e => e.Railway).HasColumnName("railway");
            entity.Property(e => e.Ref).HasColumnName("ref");
            entity.Property(e => e.Religion).HasColumnName("religion");
            entity.Property(e => e.Route).HasColumnName("route");
            entity.Property(e => e.Service).HasColumnName("service");
            entity.Property(e => e.Shop).HasColumnName("shop");
            entity.Property(e => e.Sport).HasColumnName("sport");
            entity.Property(e => e.Surface).HasColumnName("surface");
            entity.Property(e => e.Toll).HasColumnName("toll");
            entity.Property(e => e.Tourism).HasColumnName("tourism");
            entity.Property(e => e.TowerType).HasColumnName("tower:type");
            entity.Property(e => e.Tracktype).HasColumnName("tracktype");
            entity.Property(e => e.Tunnel).HasColumnName("tunnel");
            entity.Property(e => e.Water).HasColumnName("water");
            entity.Property(e => e.Waterway).HasColumnName("waterway");
            entity.Property(e => e.WayArea).HasColumnName("way_area");
            entity.Property(e => e.Wetland).HasColumnName("wetland");
            entity.Property(e => e.Width).HasColumnName("width");
            entity.Property(e => e.Wood).HasColumnName("wood");
            entity.Property(e => e.ZOrder).HasColumnName("z_order");
        });

        modelBuilder.Entity<PlanetOsmPoint>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("planet_osm_point");

            entity.Property(e => e.Access).HasColumnName("access");
            entity.Property(e => e.AddrHousename).HasColumnName("addr:housename");
            entity.Property(e => e.AddrHousenumber).HasColumnName("addr:housenumber");
            entity.Property(e => e.AddrInterpolation).HasColumnName("addr:interpolation");
            entity.Property(e => e.AdminLevel).HasColumnName("admin_level");
            entity.Property(e => e.Aerialway).HasColumnName("aerialway");
            entity.Property(e => e.Aeroway).HasColumnName("aeroway");
            entity.Property(e => e.Amenity).HasColumnName("amenity");
            entity.Property(e => e.Area).HasColumnName("area");
            entity.Property(e => e.Barrier).HasColumnName("barrier");
            entity.Property(e => e.Bicycle).HasColumnName("bicycle");
            entity.Property(e => e.Boundary).HasColumnName("boundary");
            entity.Property(e => e.Brand).HasColumnName("brand");
            entity.Property(e => e.Bridge).HasColumnName("bridge");
            entity.Property(e => e.Building).HasColumnName("building");
            entity.Property(e => e.Capital).HasColumnName("capital");
            entity.Property(e => e.Construction).HasColumnName("construction");
            entity.Property(e => e.Covered).HasColumnName("covered");
            entity.Property(e => e.Culvert).HasColumnName("culvert");
            entity.Property(e => e.Cutting).HasColumnName("cutting");
            entity.Property(e => e.Denomination).HasColumnName("denomination");
            entity.Property(e => e.Disused).HasColumnName("disused");
            entity.Property(e => e.Ele).HasColumnName("ele");
            entity.Property(e => e.Embankment).HasColumnName("embankment");
            entity.Property(e => e.Foot).HasColumnName("foot");
            entity.Property(e => e.GeneratorSource).HasColumnName("generator:source");
            entity.Property(e => e.Harbour).HasColumnName("harbour");
            entity.Property(e => e.Highway).HasColumnName("highway");
            entity.Property(e => e.Historic).HasColumnName("historic");
            entity.Property(e => e.Horse).HasColumnName("horse");
            entity.Property(e => e.Intermittent).HasColumnName("intermittent");
            entity.Property(e => e.Junction).HasColumnName("junction");
            entity.Property(e => e.Landuse).HasColumnName("landuse");
            entity.Property(e => e.Layer).HasColumnName("layer");
            entity.Property(e => e.Leisure).HasColumnName("leisure");
            entity.Property(e => e.Lock).HasColumnName("lock");
            entity.Property(e => e.ManMade).HasColumnName("man_made");
            entity.Property(e => e.Military).HasColumnName("military");
            entity.Property(e => e.Motorcar).HasColumnName("motorcar");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Natural).HasColumnName("natural");
            entity.Property(e => e.Office).HasColumnName("office");
            entity.Property(e => e.Oneway).HasColumnName("oneway");
            entity.Property(e => e.Operator).HasColumnName("operator");
            entity.Property(e => e.OsmId).HasColumnName("osm_id");
            entity.Property(e => e.Place).HasColumnName("place");
            entity.Property(e => e.Population).HasColumnName("population");
            entity.Property(e => e.Power).HasColumnName("power");
            entity.Property(e => e.PowerSource).HasColumnName("power_source");
            entity.Property(e => e.PublicTransport).HasColumnName("public_transport");
            entity.Property(e => e.Railway).HasColumnName("railway");
            entity.Property(e => e.Ref).HasColumnName("ref");
            entity.Property(e => e.Religion).HasColumnName("religion");
            entity.Property(e => e.Route).HasColumnName("route");
            entity.Property(e => e.Service).HasColumnName("service");
            entity.Property(e => e.Shop).HasColumnName("shop");
            entity.Property(e => e.Sport).HasColumnName("sport");
            entity.Property(e => e.Surface).HasColumnName("surface");
            entity.Property(e => e.Toll).HasColumnName("toll");
            entity.Property(e => e.Tourism).HasColumnName("tourism");
            entity.Property(e => e.TowerType).HasColumnName("tower:type");
            entity.Property(e => e.Tunnel).HasColumnName("tunnel");
            entity.Property(e => e.Water).HasColumnName("water");
            entity.Property(e => e.Waterway).HasColumnName("waterway");
            entity.Property(e => e.Wetland).HasColumnName("wetland");
            entity.Property(e => e.Width).HasColumnName("width");
            entity.Property(e => e.Wood).HasColumnName("wood");
            entity.Property(e => e.ZOrder).HasColumnName("z_order");
        });

        modelBuilder.Entity<PlanetOsmPolygon>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("planet_osm_polygon");

            entity.Property(e => e.Access).HasColumnName("access");
            entity.Property(e => e.AddrHousename).HasColumnName("addr:housename");
            entity.Property(e => e.AddrHousenumber).HasColumnName("addr:housenumber");
            entity.Property(e => e.AddrInterpolation).HasColumnName("addr:interpolation");
            entity.Property(e => e.AdminLevel).HasColumnName("admin_level");
            entity.Property(e => e.Aerialway).HasColumnName("aerialway");
            entity.Property(e => e.Aeroway).HasColumnName("aeroway");
            entity.Property(e => e.Amenity).HasColumnName("amenity");
            entity.Property(e => e.Area).HasColumnName("area");
            entity.Property(e => e.Barrier).HasColumnName("barrier");
            entity.Property(e => e.Bicycle).HasColumnName("bicycle");
            entity.Property(e => e.Boundary).HasColumnName("boundary");
            entity.Property(e => e.Brand).HasColumnName("brand");
            entity.Property(e => e.Bridge).HasColumnName("bridge");
            entity.Property(e => e.Building).HasColumnName("building");
            entity.Property(e => e.Construction).HasColumnName("construction");
            entity.Property(e => e.Covered).HasColumnName("covered");
            entity.Property(e => e.Culvert).HasColumnName("culvert");
            entity.Property(e => e.Cutting).HasColumnName("cutting");
            entity.Property(e => e.Denomination).HasColumnName("denomination");
            entity.Property(e => e.Disused).HasColumnName("disused");
            entity.Property(e => e.Embankment).HasColumnName("embankment");
            entity.Property(e => e.Foot).HasColumnName("foot");
            entity.Property(e => e.GeneratorSource).HasColumnName("generator:source");
            entity.Property(e => e.Harbour).HasColumnName("harbour");
            entity.Property(e => e.Highway).HasColumnName("highway");
            entity.Property(e => e.Historic).HasColumnName("historic");
            entity.Property(e => e.Horse).HasColumnName("horse");
            entity.Property(e => e.Intermittent).HasColumnName("intermittent");
            entity.Property(e => e.Junction).HasColumnName("junction");
            entity.Property(e => e.Landuse).HasColumnName("landuse");
            entity.Property(e => e.Layer).HasColumnName("layer");
            entity.Property(e => e.Leisure).HasColumnName("leisure");
            entity.Property(e => e.Lock).HasColumnName("lock");
            entity.Property(e => e.ManMade).HasColumnName("man_made");
            entity.Property(e => e.Military).HasColumnName("military");
            entity.Property(e => e.Motorcar).HasColumnName("motorcar");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Natural).HasColumnName("natural");
            entity.Property(e => e.Office).HasColumnName("office");
            entity.Property(e => e.Oneway).HasColumnName("oneway");
            entity.Property(e => e.Operator).HasColumnName("operator");
            entity.Property(e => e.OsmId).HasColumnName("osm_id");
            entity.Property(e => e.Place).HasColumnName("place");
            entity.Property(e => e.Population).HasColumnName("population");
            entity.Property(e => e.Power).HasColumnName("power");
            entity.Property(e => e.PowerSource).HasColumnName("power_source");
            entity.Property(e => e.PublicTransport).HasColumnName("public_transport");
            entity.Property(e => e.Railway).HasColumnName("railway");
            entity.Property(e => e.Ref).HasColumnName("ref");
            entity.Property(e => e.Religion).HasColumnName("religion");
            entity.Property(e => e.Route).HasColumnName("route");
            entity.Property(e => e.Service).HasColumnName("service");
            entity.Property(e => e.Shop).HasColumnName("shop");
            entity.Property(e => e.Sport).HasColumnName("sport");
            entity.Property(e => e.Surface).HasColumnName("surface");
            entity.Property(e => e.Toll).HasColumnName("toll");
            entity.Property(e => e.Tourism).HasColumnName("tourism");
            entity.Property(e => e.TowerType).HasColumnName("tower:type");
            entity.Property(e => e.Tracktype).HasColumnName("tracktype");
            entity.Property(e => e.Tunnel).HasColumnName("tunnel");
            entity.Property(e => e.Water).HasColumnName("water");
            entity.Property(e => e.Waterway).HasColumnName("waterway");
            entity.Property(e => e.WayArea).HasColumnName("way_area");
            entity.Property(e => e.Wetland).HasColumnName("wetland");
            entity.Property(e => e.Width).HasColumnName("width");
            entity.Property(e => e.Wood).HasColumnName("wood");
            entity.Property(e => e.ZOrder).HasColumnName("z_order");
        });

        modelBuilder.Entity<PlanetOsmRoad>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("planet_osm_roads");

            entity.Property(e => e.Access).HasColumnName("access");
            entity.Property(e => e.AddrHousename).HasColumnName("addr:housename");
            entity.Property(e => e.AddrHousenumber).HasColumnName("addr:housenumber");
            entity.Property(e => e.AddrInterpolation).HasColumnName("addr:interpolation");
            entity.Property(e => e.AdminLevel).HasColumnName("admin_level");
            entity.Property(e => e.Aerialway).HasColumnName("aerialway");
            entity.Property(e => e.Aeroway).HasColumnName("aeroway");
            entity.Property(e => e.Amenity).HasColumnName("amenity");
            entity.Property(e => e.Area).HasColumnName("area");
            entity.Property(e => e.Barrier).HasColumnName("barrier");
            entity.Property(e => e.Bicycle).HasColumnName("bicycle");
            entity.Property(e => e.Boundary).HasColumnName("boundary");
            entity.Property(e => e.Brand).HasColumnName("brand");
            entity.Property(e => e.Bridge).HasColumnName("bridge");
            entity.Property(e => e.Building).HasColumnName("building");
            entity.Property(e => e.Construction).HasColumnName("construction");
            entity.Property(e => e.Covered).HasColumnName("covered");
            entity.Property(e => e.Culvert).HasColumnName("culvert");
            entity.Property(e => e.Cutting).HasColumnName("cutting");
            entity.Property(e => e.Denomination).HasColumnName("denomination");
            entity.Property(e => e.Disused).HasColumnName("disused");
            entity.Property(e => e.Embankment).HasColumnName("embankment");
            entity.Property(e => e.Foot).HasColumnName("foot");
            entity.Property(e => e.GeneratorSource).HasColumnName("generator:source");
            entity.Property(e => e.Harbour).HasColumnName("harbour");
            entity.Property(e => e.Highway).HasColumnName("highway");
            entity.Property(e => e.Historic).HasColumnName("historic");
            entity.Property(e => e.Horse).HasColumnName("horse");
            entity.Property(e => e.Intermittent).HasColumnName("intermittent");
            entity.Property(e => e.Junction).HasColumnName("junction");
            entity.Property(e => e.Landuse).HasColumnName("landuse");
            entity.Property(e => e.Layer).HasColumnName("layer");
            entity.Property(e => e.Leisure).HasColumnName("leisure");
            entity.Property(e => e.Lock).HasColumnName("lock");
            entity.Property(e => e.ManMade).HasColumnName("man_made");
            entity.Property(e => e.Military).HasColumnName("military");
            entity.Property(e => e.Motorcar).HasColumnName("motorcar");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Natural).HasColumnName("natural");
            entity.Property(e => e.Office).HasColumnName("office");
            entity.Property(e => e.Oneway).HasColumnName("oneway");
            entity.Property(e => e.Operator).HasColumnName("operator");
            entity.Property(e => e.OsmId).HasColumnName("osm_id");
            entity.Property(e => e.Place).HasColumnName("place");
            entity.Property(e => e.Population).HasColumnName("population");
            entity.Property(e => e.Power).HasColumnName("power");
            entity.Property(e => e.PowerSource).HasColumnName("power_source");
            entity.Property(e => e.PublicTransport).HasColumnName("public_transport");
            entity.Property(e => e.Railway).HasColumnName("railway");
            entity.Property(e => e.Ref).HasColumnName("ref");
            entity.Property(e => e.Religion).HasColumnName("religion");
            entity.Property(e => e.Route).HasColumnName("route");
            entity.Property(e => e.Service).HasColumnName("service");
            entity.Property(e => e.Shop).HasColumnName("shop");
            entity.Property(e => e.Sport).HasColumnName("sport");
            entity.Property(e => e.Surface).HasColumnName("surface");
            entity.Property(e => e.Toll).HasColumnName("toll");
            entity.Property(e => e.Tourism).HasColumnName("tourism");
            entity.Property(e => e.TowerType).HasColumnName("tower:type");
            entity.Property(e => e.Tracktype).HasColumnName("tracktype");
            entity.Property(e => e.Tunnel).HasColumnName("tunnel");
            entity.Property(e => e.Water).HasColumnName("water");
            entity.Property(e => e.Waterway).HasColumnName("waterway");
            entity.Property(e => e.WayArea).HasColumnName("way_area");
            entity.Property(e => e.Wetland).HasColumnName("wetland");
            entity.Property(e => e.Width).HasColumnName("width");
            entity.Property(e => e.Wood).HasColumnName("wood");
            entity.Property(e => e.ZOrder).HasColumnName("z_order");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
