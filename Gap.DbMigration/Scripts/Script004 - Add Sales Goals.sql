CREATE TABLE IF NOT EXISTS gap.sales_goals (  
   id UUID DEFAULT uuid_generate_v4() NOT NULL,  
   year integer NOT NULL,  
   user_id UUID NOT NULL,  
   average_sales_price decimal(18,2) NOT NULL,  
   commission_rate decimal(18,2) NOT NULL,  
   average_commision decimal(18,2) NOT NULL,  
   average_loss_ratio decimal(18,2) NOT NULL,  
   net_sales_closed decimal(18,2) NOT NULL,  
   net_sales_needed decimal(18,2) NOT NULL,  
   gross_sales_needed decimal(18,2) NOT NULL,  
   last_modified timestamp without time zone DEFAULT '1970-01-01 08:00:00'::timestamp without time zone NOT NULL,  
   CONSTRAINT pk_sales_goals PRIMARY KEY (id, year, user_id),  
   CONSTRAINT fk_user FOREIGN KEY (user_id) REFERENCES gap.users(id)  
);

CREATE TABLE IF NOT EXISTS gap.sales_goals_quarters (  
   id UUID DEFAULT uuid_generate_v4() NOT NULL,  
   quarter integer NOT NULL,  
   sales_goals_id UUID NOT NULL,     
   gross_sales_needed decimal(18,2) NOT NULL,  
   referral integer,  
   internet integer,  
   self_originating integer,  
   realtor integer,  
   walk_in integer,  
   follow_up integer,  
   last_modified timestamp without time zone DEFAULT '1970-01-01 08:00:00'::timestamp without time zone NOT NULL,  
   CONSTRAINT pk_sales_goals_quarters PRIMARY KEY (id, quarter, sales_goals_id),  
   CONSTRAINT fk_sales_goal_id FOREIGN KEY (sales_goals_id) REFERENCES gap.sales_goals(id)  
);

