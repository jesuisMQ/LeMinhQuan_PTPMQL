package model;

import util.configs;

public class nhanvienparttime extends nhanvien{
    private int giolamviec;
    public nhanvienparttime(String ten, int giolamviec){
        this.ten=ten;
        this.giolamviec=giolamviec;
    }
    @Override
    public String loainhanvien(){
return "nhanvienparttime";
    }
    public void tinhluong(){
 luong=configs.luongmoigionhanvienparttime*giolamviec;
    }
}
