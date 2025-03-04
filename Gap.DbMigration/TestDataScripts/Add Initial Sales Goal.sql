INSERT INTO gap.sales_goals (id, year, user_id, average_sales_price, commission_rate,
average_commision, average_loss_ratio, net_sales_closed, net_sales_needed, gross_sales_needed,
last_modified)
VALUES (uuid_generate_v4(), 2025, 'd10f9784-31bd-45aa-b9a3-b4e92c4e118b', 250000, 1.75, 43750,
11.54, 88.46, 45.714, 52, Now()) ON CONFLICT DO NOTHING;