 function xlDangNhap()
 {
 	var Ten = $("#txtTen").val();
 	var Matkhau = $("#txtPass").val();
       	  	if(Ten.toLowerCase() == "thienthanh")
       	  	{
       	  		if(Matkhau == "abc")
       	  		  alert('Đăng nhập thành công');
       	  		else
       	  		  alert('Sai mật khẩu');
       	  	}
       	  	else
       	  	  alert('Không có user này!');
 }
 function ResetText(pID)
 {
 	var dieukhien = document.getElementById(pID);
 	if(dieukhien.value=='Tên sản phẩm')
 	dieukhien.value = "";
 }
 function DatLaiText(pID, Text)
 {
 	var dieukhien = document.getElementById(pID);
 	if(dieukhien.value == "")
 	  dieukhien.value = Text;
 }
 
  function xlTim()
{
	var gTri = $("#txtTensp").val();
	var chuoi="thiên thanh,shop,thời trang,giầy dép, quần áo, mắt kính";
    var chuoitim = gTri.trim().toLowerCase();
    	if(chuoi.indexOf(chuoitim) >= 0)
    	  alert('Tìm thấy');
    	else
    	  alert('Không tìm thấy');
}

 function xlTim2(e)
{
	var gTri = $("#txtTensp").val();
	var chuoi="thiên thanh,shop,thời trang,giầy dép, quần áo, mắt kính";
    var chuoitim = gTri.trim().toLowerCase();
    var c =e.keyCode;
    if(c == 13)   // 13 là mã ANSCII của phím Enter
    {
    	
    	if(chuoi.indexOf(chuoitim) >= 0)
    	  {
    	  	alert('Tìm thấy');
    	  }
    	else
    	  {
    	  	alert('Không tìm thấy');
    	  }
    }
}
function KiemTraDuLieu()
{
	var Ketqua = document.getElementById("ketqua");
	Ketqua.style.color = "black";
	var TenDN = document.getElementById("tendn");
	var MatKhau = document.getElementById("matkhau");
	var NhapLaiMatKhau = document.getElementById("nhaplaimk");
	var Hoten = document.getElementById("hoten");
	var check = true;
	var Ketqua = document.getElementById("ketqua");
	Ketqua.style.color = "red";
	var Thongbao = new String();
	if(Hoten.value == "")
	{
		Thongbao += "<li>Chưa nhập họ tên</li>";
		check = false;
	}
	if(TenDN.value == "")
	{
		Thongbao += "<li>Bạn chưa nhập tên đăng nhập</li>";
		check = false;
	}
	if(MatKhau.value.length <= 5)
	{
		if(MatKhau.value == "")
		{
			Thongbao += "<li>Bạn chưa nhập mật khẩu</li>";
		}
		else
		Thongbao += "<li>Mật khẩu phải trên 5 ký tự</li>";
		check = false;
	}
	else 
	{
		if(NhapLaiMatKhau.value == "")
		{
			Thongbao += "<li>Bạn chưa nhập lại mật khẩu</li>";
			check = false;
		}
		else 
		{
			if(NhapLaiMatKhau.value != MatKhau.value)
			{
				Thongbao += "<li>Nhập lại mật khẩu phải khớp với mật khẩu</li>";
			}
			check = false;
		}
		
	}
	if(check==false)
	{
		Ketqua.innerHTML = "<b>Danh sách lỗi</b></br><ul>";
	    Ketqua.innerHTML += Thongbao + "</ul>";
	}
	return check;
	
}
function XuLyDangKy()
{
	
	var TenDN = document.getElementById("tendn");
	var DkMatKhau = document.getElementById("matkhau");
	var Matkhau = "";
	for(i = 0; i < DkMatKhau.value.length; i++)
	{
		Matkhau += "•";
	}
	
	var Hoten = document.getElementById("hoten");
	var Gioitinh = document.getElementById("gioitinh");
	var Ngaysinh = document.getElementById("datepicker");
	var Nghenghiep = document.getElementById("ngnghiep");
	var Email =  document.getElementById("email");
	var Dienthoai =  document.getElementById("dthoai");
	var Ghichu =  document.getElementById("gchu");
	var Thongtin = "<b>Xác nhận lại thông tin</b><br/>";
	Thongtin +="<b>Tên đăng nhập: </b>" + TenDN.value + "<br/>";
	Thongtin +="<b>Mật khẩu: </b>" + Matkhau + "<br/>";;
	Thongtin += "<b>Họ tên: </b>" + Hoten.value + "<br/>";;
	Thongtin += "<b>Phái: </b>" + (Gioitinh.checked ? 'Nam':'Nữ') + "<br/>";
	Thongtin += "<b>Ngày sinh: </b>" + Ngaysinh.value + "<br/>";
	Thongtin += "<b>Nghề nghiệp: </b>" + Nghenghiep.options[Nghenghiep.selectedIndex].label + "<br/>" ;
	Thongtin += "<b>Email: </b>" + Email.value + "<br/>";
	Thongtin += "<b>Điện thoại: </b>" + Dienthoai.value + "<br/>";;
	Thongtin += "<b>Ghi chú: </b>" + Ghichu.value + "<br/>";;
	return Thongtin;
}
