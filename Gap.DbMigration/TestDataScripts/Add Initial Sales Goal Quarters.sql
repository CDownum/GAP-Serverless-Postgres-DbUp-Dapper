Insert INTO gap.sales_goals_quarters (id, quarter, sales_goals_id, gross_sales_needed, referral, 
internet, self_originating, realtor, walk_in, follow_up, last_modified)
Values (uuid_generate_v4(), 1, '4c578194-ae34-4d67-8ef4-eb179721dddd', 
16, 4, 1, 3, 4, 2, 2, Now()) ON CONFLICT DO NOTHING;

Insert INTO gap.sales_goals_quarters (id, quarter, sales_goals_id, gross_sales_needed, referral, 
internet, self_originating, realtor, walk_in, follow_up, last_modified)
Values (uuid_generate_v4(), 2, '4c578194-ae34-4d67-8ef4-eb179721dddd', 
14, 4, 1, 3, 3, 1, 2, Now()) ON CONFLICT DO NOTHING;

Insert INTO gap.sales_goals_quarters (id, quarter, sales_goals_id, gross_sales_needed, referral, 
internet, self_originating, realtor, walk_in, follow_up, last_modified)
Values (uuid_generate_v4(), 3, '4c578194-ae34-4d67-8ef4-eb179721dddd', 
12, 4, 1, 2, 2, 1, 1, Now()) ON CONFLICT DO NOTHING;

Insert INTO gap.sales_goals_quarters (id, quarter, sales_goals_id, gross_sales_needed, referral, 
internet, self_originating, realtor, walk_in, follow_up, last_modified)
Values (uuid_generate_v4(), 4, '4c578194-ae34-4d67-8ef4-eb179721dddd', 
10, 4, 1, 1, 1, 0, 1, Now()) ON CONFLICT DO NOTHING;

