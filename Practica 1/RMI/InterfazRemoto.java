import java.rmi.Remote;

public interface InterfazRemoto extends Remote {
    public Integer getVolumen() throws java.rmi.RemoteException;
    public String getFecha() throws java.rmi.RemoteException;
    public String getUltimaFecha() throws java.rmi.RemoteException;
    public String getLuz() throws java.rmi.RemoteException;
    public void setLuz(String luz) throws java.rmi.RemoteException;
}