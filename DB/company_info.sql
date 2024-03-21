-- 公司基本資料
-- auto-generated definition
create table company_info
(
    id                uuid default gen_random_uuid() not null,
    stock_code        varchar                        not null,
    name              varchar                        not null,
    alias             varchar                        not null,
    industry_type     varchar                        not null,
    chair_man         varchar                        not null,
    general_manager   integer                        not null,
    spokes_man        varchar                        not null,
    contact_telephone varchar                        not null,
    begin_date        date                           not null,
    public_date       date                           not null,
    capital           numeric                        not null,
    contact_email     varchar                        not null,
    website           varchar                        not null,
    create_datetime   timestamp with time zone,
    update_datetime   timestamp with time zone
);

comment on table company_info is '公司基本資料';

comment on column company_info.id is '唯一值';

comment on column company_info.stock_code is '股號';

comment on column company_info.name is '公司全名';

comment on column company_info.alias is '公司簡稱';

comment on column company_info.industry_type is '產業別';

comment on column company_info.general_manager is '總經理';

comment on column company_info.spokes_man is '發言人';

comment on column company_info.begin_date is '成立日期';

comment on column company_info.public_date is '上市日期';

comment on column company_info.capital is '實收資本額';

comment on column company_info.website is '網址';

alter table company_info
    owner to postgres;