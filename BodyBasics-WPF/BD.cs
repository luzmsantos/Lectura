using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using MySql.Data.MySqlClient;
using Microsoft.Kinect;

namespace Microsoft.Samples.Kinect.BodyBasics
{
    class BD
    {
        //liga conexion BD.
        //  BodyBasics.Connection1 conn = new BodyBasics.Connection1();
       private BodyBasics.Connection1 conn;

       public BD()
       {
           conn = new BodyBasics.Connection1();
       }

       public void insertar (string idlectura, string nombre,string gesto, float headx,float heady,float headz,float neckx,float necky,float neckz, float hipleftx,float hiplefty, float hipleftz, float footleftx,float footlefty, float footleftz,float handleftx,float handlefty,float handleftz,float hiprightx,float hiprighty,float hiprightz,
           float kneeleftx, float kneelefty, float kneeleftz, float spinemidx, float spinemidy, float spinemidz, float ankleleftx, float anklelefty, float ankleleftz, float elbowleftx, float elbowlefty, float elbowleftz, float footrightx, float footrighty, float footrightz, float handrightx, float handrighty, float handrightz,
           float kneerightx, float kneerighty, float kneerightz, float spinebasex, float spinebasey, float spinebasez, float thumbleftx, float thumblefty, float thumbleftz, float wristleftx, float wristlefty, float wristleftz, float anklerightx, float anklerighty, float anklerightz, float elbowrightx, float elbowrighty,
           float elbowrightz, float thumbrightx, float thumbrighty, float thumbrightz, float wristrightx, float wristrighty, float wristrightz, float handtipleftx, float handtiplefty, float handtipleftz, float handtiprightx, float handtiprighty, float handtiprightz, float shoulderleftx, float shoulderlefty,
           float shoulderleftz, float shoulderrightx, float shoulderrighty, float shoulderrightz, float spineshoulderx, float spineshouldery, float spineshoulderz) 
           
       {
           MySqlCommand command = new MySqlCommand();
           command.CommandType = System.Data.CommandType.Text;

           command.CommandText = "INSERT INTO frame(idlectura,nombre,gesto,headx,heady,headz,neckx,necky,neckz,hipleftx,hiplefty,hipleftz,footleftx,footlefty,footleftz,handleftx,handlefty,handleftz,hiprightx,hiprighty,hiprightz,kneeleftx,kneelefty,kneeleftz,spinemidx,spinemidy,spinemidz,ankleleftx,anklelefty,ankleleftz,elbowleftx,elbowlefty,elbowleftz,footrightx,footrighty,footrightz,handrightx,handrighty,handrightz,kneerightx,kneerighty,kneerightz,spinebasex,spinebasey,spinebasez,thumbleftx,thumblefty,thumbleftz,wristleftx,wristlefty,wristleftz,anklerightx,anklerighty,anklerightz,elbowrightx,elbowrighty,elbowrightz,thumbrightx,thumbrighty,thumbrightz,wristrightx,wristrighty,wristrightz,handtipleftx,handtiplefty,handtipleftz,handtiprightx,handtiprighty,handtiprightz,shoulderleftx,shoulderlefty,shoulderleftz,shoulderrightx,shoulderrighty,shoulderrightz,spineshoulderx,spineshouldery,spineshoulderz) VALUES (@idlectura,@nombre,@gesto, @headx,@heady,@headz,@neckx, @necky,@neckz,@hipleftx,@hiplefty,@hipleftz,@footleftx,@footlefty,@footleftz,@handleftx,@handlefty,@handleftz,@hiprightx,@hiprighty,@hiprightz,@kneeleftx,@kneelefty,@kneeleftz,@spinemidx,@spinemidy,@spinemidz,@ankleleftx,@anklelefty, @ankleleftz, @elbowleftx, @elbowlefty,@elbowleftz, @footrightx, @footrighty, @footrightz, @handrightx, @handrighty, @handrightz, @kneerightx, @kneerighty, @kneerightz, @spinebasex, @spinebasey,@spinebasez, @thumbleftx, @thumblefty, @thumbleftz, @wristleftx, @wristlefty, @wristleftz, @anklerightx, @anklerighty, @anklerightz, @elbowrightx, @elbowrighty,@elbowrightz, @thumbrightx, @thumbrighty, @thumbrightz, @wristrightx, @wristrighty, @wristrightz, @handtipleftx, @handtiplefty, @handtipleftz, @handtiprightx,@handtiprighty, @handtiprightz, @shoulderleftx, @shoulderlefty, @shoulderleftz, @shoulderrightx, @shoulderrighty, @shoulderrightz, @spineshoulderx, @spineshouldery, @spineshoulderz);";

           //command.CommandText = "INSERT INTO brazosabiertos(idlectura,headx,heady,headz,neckx,necky,neckz,hipleftx,hiplefty,hipleftz,footleftx,footlefty,footleftz,handleftx,handlefty,handleftz,hiprightx,hiprighty,hiprightz,kneeleftx,kneelefty,kneeleftz,spinemidx,spinemidy,spinemidz,ankleleftx,anklelefty,ankleleftz,elbowleftx,elbowlefty,elbowleftz,footrightx,footrighty,footrightz,handrightx,handrighty,handrightz,kneerightx,kneerighty,kneerightz,spinebasex,spinebasey,spinebasez,thumbleftx,thumblefty,thumbleftz,wristleftx,wristlefty,wristleftz,anklerightx,anklerighty,anklerightz,elbowrightx,elbowrighty,elbowrightz,thumbrightx,thumbrighty,thumbrightz,wristrightx,wristrighty,wristrightz,handtipleftx,handtiplefty,handtipleftz,handtiprightx,handtiprighty,handtiprightz,shoulderleftx,shoulderlefty,shoulderleftz,shoulderrightx,shoulderrighty,shoulderrightz,spineshoulderx,spineshouldery,spineshoulderz) VALUES (@idlectura,@headx,@heady,@headz,@neckx, @necky,@neckz,@hipleftx,@hiplefty,@hipleftz,@footleftx,@footlefty,@footleftz,@handleftx,@handlefty,@handleftz,@hiprightx,@hiprighty,@hiprightz,@kneeleftx,@kneelefty,@kneeleftz,@spinemidx,@spinemidy,@spinemidz,@ankleleftx,@anklelefty, @ankleleftz, @elbowleftx, @elbowlefty,@elbowleftz, @footrightx, @footrighty, @footrightz, @handrightx, @handrighty, @handrightz, @kneerightx, @kneerighty, @kneerightz, @spinebasex, @spinebasey,@spinebasez, @thumbleftx, @thumblefty, @thumbleftz, @wristleftx, @wristlefty, @wristleftz, @anklerightx, @anklerighty, @anklerightz, @elbowrightx, @elbowrighty,@elbowrightz, @thumbrightx, @thumbrighty, @thumbrightz, @wristrightx, @wristrighty, @wristrightz, @handtipleftx, @handtiplefty, @handtipleftz, @handtiprightx,@handtiprighty, @handtiprightz, @shoulderleftx, @shoulderlefty, @shoulderleftz, @shoulderrightx, @shoulderrighty, @shoulderrightz, @spineshoulderx, @spineshouldery, @spineshoulderz);";
           //command.CommandText = "INSERT INTO brazosarriba(idlectura,headx,heady,headz,neckx,necky,neckz,hipleftx,hiplefty,hipleftz,footleftx,footlefty,footleftz,handleftx,handlefty,handleftz,hiprightx,hiprighty,hiprightz,kneeleftx,kneelefty,kneeleftz,spinemidx,spinemidy,spinemidz,ankleleftx,anklelefty,ankleleftz,elbowleftx,elbowlefty,elbowleftz,footrightx,footrighty,footrightz,handrightx,handrighty,handrightz,kneerightx,kneerighty,kneerightz,spinebasex,spinebasey,spinebasez,thumbleftx,thumblefty,thumbleftz,wristleftx,wristlefty,wristleftz,anklerightx,anklerighty,anklerightz,elbowrightx,elbowrighty,elbowrightz,thumbrightx,thumbrighty,thumbrightz,wristrightx,wristrighty,wristrightz,handtipleftx,handtiplefty,handtipleftz,handtiprightx,handtiprighty,handtiprightz,shoulderleftx,shoulderlefty,shoulderleftz,shoulderrightx,shoulderrighty,shoulderrightz,spineshoulderx,spineshouldery,spineshoulderz) VALUES (@idlectura,@headx,@heady,@headz,@neckx, @necky,@neckz,@hipleftx,@hiplefty,@hipleftz,@footleftx,@footlefty,@footleftz,@handleftx,@handlefty,@handleftz,@hiprightx,@hiprighty,@hiprightz,@kneeleftx,@kneelefty,@kneeleftz,@spinemidx,@spinemidy,@spinemidz,@ankleleftx,@anklelefty, @ankleleftz, @elbowleftx, @elbowlefty,@elbowleftz, @footrightx, @footrighty, @footrightz, @handrightx, @handrighty, @handrightz, @kneerightx, @kneerighty, @kneerightz, @spinebasex, @spinebasey,@spinebasez, @thumbleftx, @thumblefty, @thumbleftz, @wristleftx, @wristlefty, @wristleftz, @anklerightx, @anklerighty, @anklerightz, @elbowrightx, @elbowrighty,@elbowrightz, @thumbrightx, @thumbrighty, @thumbrightz, @wristrightx, @wristrighty, @wristrightz, @handtipleftx, @handtiplefty, @handtipleftz, @handtiprightx,@handtiprighty, @handtiprightz, @shoulderleftx, @shoulderlefty, @shoulderleftz, @shoulderrightx, @shoulderrighty, @shoulderrightz, @spineshoulderx, @spineshouldery, @spineshoulderz);";
           //command.CommandText = "INSERT INTO manolevantada(idlectura,headx,heady,headz,neckx,necky,neckz,hipleftx,hiplefty,hipleftz,footleftx,footlefty,footleftz,handleftx,handlefty,handleftz,hiprightx,hiprighty,hiprightz,kneeleftx,kneelefty,kneeleftz,spinemidx,spinemidy,spinemidz,ankleleftx,anklelefty,ankleleftz,elbowleftx,elbowlefty,elbowleftz,footrightx,footrighty,footrightz,handrightx,handrighty,handrightz,kneerightx,kneerighty,kneerightz,spinebasex,spinebasey,spinebasez,thumbleftx,thumblefty,thumbleftz,wristleftx,wristlefty,wristleftz,anklerightx,anklerighty,anklerightz,elbowrightx,elbowrighty,elbowrightz,thumbrightx,thumbrighty,thumbrightz,wristrightx,wristrighty,wristrightz,handtipleftx,handtiplefty,handtipleftz,handtiprightx,handtiprighty,handtiprightz,shoulderleftx,shoulderlefty,shoulderleftz,shoulderrightx,shoulderrighty,shoulderrightz,spineshoulderx,spineshouldery,spineshoulderz) VALUES (@idlectura,@headx,@heady,@headz,@neckx, @necky,@neckz,@hipleftx,@hiplefty,@hipleftz,@footleftx,@footlefty,@footleftz,@handleftx,@handlefty,@handleftz,@hiprightx,@hiprighty,@hiprightz,@kneeleftx,@kneelefty,@kneeleftz,@spinemidx,@spinemidy,@spinemidz,@ankleleftx,@anklelefty, @ankleleftz, @elbowleftx, @elbowlefty,@elbowleftz, @footrightx, @footrighty, @footrightz, @handrightx, @handrighty, @handrightz, @kneerightx, @kneerighty, @kneerightz, @spinebasex, @spinebasey,@spinebasez, @thumbleftx, @thumblefty, @thumbleftz, @wristleftx, @wristlefty, @wristleftz, @anklerightx, @anklerighty, @anklerightz, @elbowrightx, @elbowrighty,@elbowrightz, @thumbrightx, @thumbrighty, @thumbrightz, @wristrightx, @wristrighty, @wristrightz, @handtipleftx, @handtiplefty, @handtipleftz, @handtiprightx,@handtiprighty, @handtiprightz, @shoulderleftx, @shoulderlefty, @shoulderleftz, @shoulderrightx, @shoulderrighty, @shoulderrightz, @spineshoulderx, @spineshouldery, @spineshoulderz);";    
           

           command.Parameters.Add("@idlectura", MySqlDbType.String, 35);
           command.Parameters["@idlectura"].Value = idlectura;
           
           command.Parameters.Add("@nombre", MySqlDbType.String, 35);
           command.Parameters["@nombre"].Value = nombre;

           command.Parameters.Add("@gesto", MySqlDbType.String, 35);
           command.Parameters["@gesto"].Value = gesto;

           command.Parameters.Add("@headx", MySqlDbType.Float);
           command.Parameters["@headx"].Value = headx;
           command.Parameters.Add("@heady", MySqlDbType.Float);
           command.Parameters["@heady"].Value = heady;
           command.Parameters.Add("@headz", MySqlDbType.Float);
           command.Parameters["@headz"].Value = headz;

           command.Parameters.Add("@neckx", MySqlDbType.Float);
           command.Parameters["@neckx"].Value = neckx;
           command.Parameters.Add("@necky", MySqlDbType.Float);
           command.Parameters["@necky"].Value = necky;
           command.Parameters.Add("@neckz", MySqlDbType.Float);
           command.Parameters["@neckz"].Value = neckz;

           command.Parameters.Add("@hipleftx", MySqlDbType.Float);
           command.Parameters["@hipleftx"].Value = hipleftx;
           command.Parameters.Add("@hiplefty", MySqlDbType.Float);
           command.Parameters["@hiplefty"].Value = hiplefty;
           command.Parameters.Add("@hipleftz", MySqlDbType.Float);
           command.Parameters["@hipleftz"].Value = hipleftz;

           command.Parameters.Add("@footleftx", MySqlDbType.Float);
           command.Parameters["@footleftx"].Value = footleftx;
           command.Parameters.Add("@footlefty", MySqlDbType.Float);
           command.Parameters["@footlefty"].Value = footlefty;
           command.Parameters.Add("@footleftz", MySqlDbType.Float);
           command.Parameters["@footleftz"].Value = footleftz;

           command.Parameters.Add("@handleftx", MySqlDbType.Float);
           command.Parameters["@handleftx"].Value = handleftx;
           command.Parameters.Add("@handlefty", MySqlDbType.Float);
           command.Parameters["@handlefty"].Value = handlefty;
           command.Parameters.Add("@handleftz", MySqlDbType.Float);
           command.Parameters["@handleftz"].Value = handleftz;

           command.Parameters.Add("@hiprightx", MySqlDbType.Float);
           command.Parameters["@hiprightx"].Value = hiprightx;
           command.Parameters.Add("@hiprighty", MySqlDbType.Float);
           command.Parameters["@hiprighty"].Value = hiprighty;
           command.Parameters.Add("@hiprightz", MySqlDbType.Float);
           command.Parameters["@hiprightz"].Value = hiprightz;

           command.Parameters.Add("@kneeleftx", MySqlDbType.Float);
           command.Parameters["@kneeleftx"].Value = kneeleftx;
           command.Parameters.Add("@kneelefty", MySqlDbType.Float);
           command.Parameters["@kneelefty"].Value = kneelefty;
           command.Parameters.Add("@kneeleftz", MySqlDbType.Float);
           command.Parameters["@kneeleftz"].Value = kneeleftz;

           command.Parameters.Add("@spinemidx", MySqlDbType.Float);
           command.Parameters["@spinemidx"].Value = spinemidx;
           command.Parameters.Add("@spinemidy", MySqlDbType.Float);
           command.Parameters["@spinemidy"].Value = spinemidy;
           command.Parameters.Add("@spinemidz", MySqlDbType.Float);
           command.Parameters["@spinemidz"].Value = spinemidz;

           command.Parameters.Add("@ankleleftx", MySqlDbType.Float);
           command.Parameters["@ankleleftx"].Value = ankleleftx;
           command.Parameters.Add("@anklelefty", MySqlDbType.Float);
           command.Parameters["@anklelefty"].Value = anklelefty;
           command.Parameters.Add("@ankleleftz", MySqlDbType.Float);
           command.Parameters["@ankleleftz"].Value = ankleleftz;

           command.Parameters.Add("@elbowleftx", MySqlDbType.Float);
           command.Parameters["@elbowleftx"].Value = elbowleftx;
           command.Parameters.Add("@elbowlefty", MySqlDbType.Float);
           command.Parameters["@elbowlefty"].Value = elbowlefty;
           command.Parameters.Add("@elbowleftz", MySqlDbType.Float);
           command.Parameters["@elbowleftz"].Value = elbowleftz;

           command.Parameters.Add("@footrightx", MySqlDbType.Float);
           command.Parameters["@footrightx"].Value = footrightx;
           command.Parameters.Add("@footrighty", MySqlDbType.Float);
           command.Parameters["@footrighty"].Value = footrighty;
           command.Parameters.Add("@footrightz", MySqlDbType.Float);
           command.Parameters["@footrightz"].Value = footrightz;

           command.Parameters.Add("@handrightx", MySqlDbType.Float);
           command.Parameters["@handrightx"].Value = handrightx;
           command.Parameters.Add("@handrighty", MySqlDbType.Float);
           command.Parameters["@handrighty"].Value = handrighty;
           command.Parameters.Add("@handrightz", MySqlDbType.Float);
           command.Parameters["@handrightz"].Value = handrightz;

           command.Parameters.Add("@kneerightx", MySqlDbType.Float);
           command.Parameters["@kneerightx"].Value = kneerightx;
           command.Parameters.Add("@kneerighty", MySqlDbType.Float);
           command.Parameters["@kneerighty"].Value = kneerighty;
           command.Parameters.Add("@kneerightz", MySqlDbType.Float);
           command.Parameters["@kneerightz"].Value = kneerightz;

           command.Parameters.Add("@spinebasex", MySqlDbType.Float);
           command.Parameters["@spinebasex"].Value = spinebasex;
           command.Parameters.Add("@spinebasey", MySqlDbType.Float);
           command.Parameters["@spinebasey"].Value = spinebasey;
           command.Parameters.Add("@spinebasez", MySqlDbType.Float);
           command.Parameters["@spinebasez"].Value = spinebasez;

           command.Parameters.Add("@thumbleftx", MySqlDbType.Float);
           command.Parameters["@thumbleftx"].Value = thumbleftx;
           command.Parameters.Add("@thumblefty", MySqlDbType.Float);
           command.Parameters["@thumblefty"].Value = thumblefty;
           command.Parameters.Add("@thumbleftz", MySqlDbType.Float);
           command.Parameters["@thumbleftz"].Value = thumbleftz;

           command.Parameters.Add("@wristleftx", MySqlDbType.Float);
           command.Parameters["@wristleftx"].Value = wristleftx;
           command.Parameters.Add("@wristlefty", MySqlDbType.Float);
           command.Parameters["@wristlefty"].Value = wristlefty;
           command.Parameters.Add("@wristleftz", MySqlDbType.Float);
           command.Parameters["@wristleftz"].Value = wristlefty;

           command.Parameters.Add("@anklerightx", MySqlDbType.Float);
           command.Parameters["@anklerightx"].Value = anklerightx;
           command.Parameters.Add("@anklerighty", MySqlDbType.Float);
           command.Parameters["@anklerighty"].Value = anklerighty;
           command.Parameters.Add("@anklerightz", MySqlDbType.Float);
           command.Parameters["@anklerightz"].Value = anklerightz;

           command.Parameters.Add("@elbowrightx", MySqlDbType.Float);
           command.Parameters["@elbowrightx"].Value = elbowrightx;
           command.Parameters.Add("@elbowrighty", MySqlDbType.Float);
           command.Parameters["@elbowrighty"].Value = elbowrighty;
           command.Parameters.Add("@elbowrightz", MySqlDbType.Float);
           command.Parameters["@elbowrightz"].Value = elbowrightz;

           command.Parameters.Add("@thumbrightx", MySqlDbType.Float);
           command.Parameters["@thumbrightx"].Value = thumbrightx;
           command.Parameters.Add("@thumbrighty", MySqlDbType.Float);
           command.Parameters["@thumbrighty"].Value = thumbrighty;
           command.Parameters.Add("@thumbrightz", MySqlDbType.Float);
           command.Parameters["@thumbrightz"].Value = thumbrightz;

           command.Parameters.Add("@wristrightx", MySqlDbType.Float);
           command.Parameters["@wristrightx"].Value = wristrightx;
           command.Parameters.Add("@wristrighty", MySqlDbType.Float);
           command.Parameters["@wristrighty"].Value = wristrighty;
           command.Parameters.Add("@wristrightz", MySqlDbType.Float);
           command.Parameters["@wristrightz"].Value = wristrightz;

           command.Parameters.Add("@handtipleftx", MySqlDbType.Float);
           command.Parameters["@handtipleftx"].Value = handtipleftx;
           command.Parameters.Add("@handtiplefty", MySqlDbType.Float);
           command.Parameters["@handtiplefty"].Value = handtiplefty;
           command.Parameters.Add("@handtipleftz", MySqlDbType.Float);
           command.Parameters["@handtipleftz"].Value = handtipleftz;

           command.Parameters.Add("@handtiprightx", MySqlDbType.Float);
           command.Parameters["@handtiprightx"].Value = handtiprightx;
           command.Parameters.Add("@handtiprighty", MySqlDbType.Float);
           command.Parameters["@handtiprighty"].Value = handtiprighty;
           command.Parameters.Add("@handtiprightz", MySqlDbType.Float);
           command.Parameters["@handtiprightz"].Value = handtiprightz;

           command.Parameters.Add("@shoulderleftx", MySqlDbType.Float);
           command.Parameters["@shoulderleftx"].Value = shoulderleftx;
           command.Parameters.Add("@shoulderlefty", MySqlDbType.Float);
           command.Parameters["@shoulderlefty"].Value = shoulderlefty;
           command.Parameters.Add("@shoulderleftz", MySqlDbType.Float);
           command.Parameters["@shoulderleftz"].Value = shoulderleftz;

           command.Parameters.Add("@shoulderrightx", MySqlDbType.Float);
           command.Parameters["@shoulderrightx"].Value = shoulderrightx;
           command.Parameters.Add("@shoulderrighty", MySqlDbType.Float);
           command.Parameters["@shoulderrighty"].Value = shoulderrighty;
           command.Parameters.Add("@shoulderrightz", MySqlDbType.Float);
           command.Parameters["@shoulderrightz"].Value = shoulderrightz;

           command.Parameters.Add("@spineshoulderx", MySqlDbType.Float);
           command.Parameters["@spineshoulderx"].Value = spineshoulderx;
           command.Parameters.Add("@spineshouldery", MySqlDbType.Float);
           command.Parameters["@spineshouldery"].Value = spineshouldery;
           command.Parameters.Add("@spineshoulderz", MySqlDbType.Float);
           command.Parameters["@spineshoulderz"].Value = spineshoulderz;
           command.Connection = conn.conect;
           conn.connect();
           command.ExecuteNonQuery();
           conn.desconectar();

       }
    }
}
