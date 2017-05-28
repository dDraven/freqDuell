package com.dad.geburtstag.familienduell.TCPClient;

import android.util.Log;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.DataOutputStream;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.io.PrintWriter;
import java.net.InetAddress;
import java.net.Socket;

/**
 * Created by Dave on 21.01.2016.
 */
public class TCPClient {


    private String serverMessage;
    public static final String SERVERIP = "192.168.178.53";
    public static final int SERVERPORT = 4711;
    private OnMessageReceived mMessageListener = null;
    private boolean mRun = false;

    PrintWriter out;
    BufferedReader in;
    DataOutputStream dataOutputStream = null;

    //OutputStream os;

    public TCPClient(OnMessageReceived listener) {
        mMessageListener = listener;
    }

    public void sendMessage(String message){
        if (out != null && !out.checkError()) {

            /*
            String toSend = "a";
            byte[] toSendBytes = toSend.getBytes();

            int toSendLen = toSendBytes.length;
            byte[] toSendLenBytes = new byte[4];
            toSendLenBytes[0] = (byte)(toSendLen & 0xff);
            toSendLenBytes[1] = (byte)((toSendLen >> 8) & 0xff);
            toSendLenBytes[2] = (byte)((toSendLen >> 16) & 0xff);
            toSendLenBytes[3] = (byte)((toSendLen >> 24) & 0xff);
            try {
                os.write(toSendLenBytes);
            } catch (IOException e1) {
                // TODO Auto-generated catch block
                e1.printStackTrace();
            }

            try {
                os.write(toSendBytes);
            } catch (IOException e) {
                // TODO Auto-generated catch block
                e.printStackTrace();
            }
            */
            out.print(message);
            out.flush();


        }
    }

    public void stopClient(){
        mRun = false;
    }

    public void run() {

        mRun = true;

        try {

            InetAddress serverAddr = InetAddress.getByName(SERVERIP);

            Log.e("TCP Client", "C: Connecting...");

            Socket socket = new Socket(serverAddr, SERVERPORT);

            ///
            //os = socket.getOutputStream();

            if(socket.isBound()){
                Log.i("SOCKET", "Socket: Connected");
            }
            else{
                Log.e("SOCKET", "Socket: Not Connected");
            }
            try {

                out = new PrintWriter(new BufferedWriter(new OutputStreamWriter(socket.getOutputStream())), true);

                /*
                dataOutputStream = new DataOutputStream(socket.getOutputStream());
                byte[] bytes = new byte[] {1};
                dataOutputStream.write(bytes, 0, bytes.length);
                */
                Log.e("TCP Client", "C: Sent.");

                Log.e("TCP Client", "C: Done.");

                if(out.checkError())
                {
                    Log.e("PrintWriter", "CheckError");
                }

                in = new BufferedReader(new InputStreamReader(socket.getInputStream()));

                serverMessage = in.readLine();

                while (mRun) {
                    serverMessage = in.readLine();

                    if (serverMessage != null && mMessageListener != null) {
                        mMessageListener.messageReceived(serverMessage);
                    }
                    serverMessage = null;

                }


                Log.e("RESPONSE FROM SERVER", "S: Received Message: '" + serverMessage + "'");


            } catch (Exception e) {

                Log.e("TCP", "S: Error", e);

            } finally {

                socket.close();
            }

        } catch (Exception e) {

            Log.e("TCP", "C: Error", e);

        }

    }

    public interface OnMessageReceived {
        public void messageReceived(String message);
    }
}
