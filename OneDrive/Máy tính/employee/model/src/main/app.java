package main;
import model.nhanvienfulltime;
import util.configs;
public class app{
    public static void main(String[] args) {
        nhanvienfulltime nhanvien1 = new nhanvienfulltime("le minh quan", 5);
        nhanvien1.setloaichucvu(1);
        nhanvien1.tinhLuong();
        nhanvien1.xuatthongtin();
    }
}