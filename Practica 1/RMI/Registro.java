import java.rmi.*;


public class Registro {
    public static void main (String args[]){
        String URLRegistro;
        try{
            //System.setSecurityManager(new RMISecurityManager());
            System.setSecurityManager(new SecurityManager());

            Integer tamanyo = Naming.list("rmi://localhost:1099").length;
            for (int i = 0; i < tamanyo; i++){
                URLRegistro = "/ObjetoRemoto" + (tamanyo-i);
                Naming.unbind(URLRegistro);
            }

            for (int j = 1; j<=3; j++){
                ObjetoRemoto objetoRemoto = new ObjetoRemoto(j);
                URLRegistro = "/ObjetoRemoto" + j;
                Naming.rebind(URLRegistro, objetoRemoto);
                System.out.println("Servidor de objeto preparado.");
            }
        }
        catch (Exception e){
            System.out.println(e);
        }     
    }
}