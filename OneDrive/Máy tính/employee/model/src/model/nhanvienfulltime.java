
package model;

import util.configs;


public class nhanvienfulltime extends nhanvien{
public String ten;
    private int ngaylamthem;
    private int loaichucvu;
   
    
    public  nhanvienfulltime(String ten, int ngaylamthem) {
       this.ten=ten;
       this.ngaylamthem=ngaylamthem;
       
   
    }
    public void setloaichucvu(int loaichucvu){
      

    }
       
@Override
    public String loainhanvien(){
        return "nhanvienfulltime";
    }
     
    public void tinhLuong(){
        if(loaichucvu==1){
            luong=configs.luongcungnhanvienfulltime+configs.luonglamthemmoingaynhanvienfulltime*ngaylamthem;
        }
        else{
            luong=configs.luonggiamdoc;
        }
     
    }
    
}


