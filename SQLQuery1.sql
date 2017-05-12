select a.客戶名稱,c.姓名,b.銀行名稱 from 客戶資料 a join 客戶銀行資訊 b on a.Id=b.客戶Id join 客戶聯絡人 c on a.Id=c.客戶Id 
select a.客戶名稱,c.姓名 from 客戶資料 a join 客戶聯絡人 c on a.Id=c.客戶Id 
select a.客戶名稱,b.銀行名稱 from 客戶資料 a join 客戶銀行資訊 b on a.Id=b.客戶Id 

select a.客戶名稱,count(c.姓名) 聯絡人數量  from 客戶資料 a  left join 客戶聯絡人 c on a.Id=c.客戶Id group by a.客戶名稱

--select a.客戶名稱, count(c.姓名) 聯絡人數量, count(b.Id) 帳戶數量  from 客戶資料 a left join 客戶銀行資訊 b on a.Id=b.客戶Id left join 客戶聯絡人 c on a.Id=c.客戶Id group by a.客戶名稱

select a.客戶名稱, count(b.銀行名稱) 帳戶數量  from 客戶資料 a left join 客戶銀行資訊 b on a.Id=b.客戶Id  group by a.客戶名稱




select a.客戶名稱,c.姓名 from 客戶資料 a join 客戶聯絡人 c on a.Id=c.客戶Id 
select a.客戶名稱,b.銀行名稱 from 客戶資料 a join 客戶銀行資訊 b on a.Id=b.客戶Id 

select 客戶名稱,
 (select count(*)  from 客戶聯絡人 where 客戶Id= 客戶資料.Id ) 聯絡人數量,
 (select count(*)  from 客戶銀行資訊 where 客戶Id= 客戶資料.Id ) 帳戶數量
  from 客戶資料


