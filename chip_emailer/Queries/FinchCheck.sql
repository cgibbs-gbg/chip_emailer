select 
ftdna_edi_order.order_id AS "OrderId",
ftdna_edi_order.grc_number AS "GrcNumber",
ftdna_edi_panel.panel AS "Panel",
ftdna_edi_order.marker AS "Marker",
ftdna_edi_order.priority AS "Priority",
ftdna_edi_order.order_date AS "OrderDate",
ftdna_edi_order.req_id AS "ReqId",
ftdna_edi_order.allele_id AS "AlleleId",
ftdna_edi_order.batch as "Batch"
FROM finch.ftdna_edi_order
  join finch.ftdna_edi_panel on ftdna_edi_panel.marker = ftdna_edi_order.marker and ftdna_edi_panel.panel = 'ChipGXGGSAv2-2'
  where order_date <= localtimestamp - interval '2 week'
  and order_date > localtimestamp - interval '6 month'
  and not exists (
    SELECT 1
    FROM finch.adb_allele
    JOIN finch.adb_sample on adb_sample.sample_id = adb_allele.sample_id
    join finch.adb_marker on adb_marker.marker_id = adb_allele.marker_id and adb_marker.name = ftdna_edi_order.marker
    and adb_sample.name = ftdna_edi_order.grc_number
    and adb_allele.allele = 'Complete'
    and adb_allele.edit_time > ftdna_edi_order.order_date
  )
order by order_date desc
;