package model;

public class nhanvien {
protected  String ten;

protected long luong;
public  nhanvien(){

}
public  nhanvien(String ten){
    this.ten=ten;
}
protected String loainhanvien(){
    return "";
}
public void xuatthongtin(){
    System.out.println("nhan vien:"+ten);
    System.out.println("loai nhan vien:"+loainhanvien());
    System.out.println("luong "+luong+"vnd");
}

}
