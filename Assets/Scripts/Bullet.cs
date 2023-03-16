using System.Collections;
using System.Collections.Generic;
using Common2;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class Bullet : MonoBehaviour
{
   
   private bool m_islocal;

   public bool MIslocal
   {
      get => m_islocal;
      set => m_islocal = value;
   }

   private void OnTriggerEnter(Collider collider)
   {
      //对人物攻击检测名字对玩家的血量条，把子弹赋值一个伤害值
      if (collider.CompareTag("Player"))
      {Debug.Log("无");
         //累计得分，判断玩家是否死亡
         collider.transform.Find("Health/HP").GetComponent<Image>().fillAmount =
            collider.transform.Find("Health/HP").GetComponent<Image>().fillAmount - 0.2f;
         if (collider.transform.Find("Health/HP").GetComponent<Image>().fillAmount < 0.01f)
         {
            if (MIslocal)
            {
               Debug.Log("玩家打死敌人");
                  //记录杀敌数量
                  PlayerManage.instance.KillNum++;
                  //当前客户端玩家列表移除
                  PlayerManage.instance.PlyerDic.Remove(
                     collider.transform.Find("Health/Name").GetComponent<Text>().text);
                  
                  //销毁被打死玩家
                  Destroy(collider.gameObject);
            }
            else
            {
               if (collider.transform.Find("Health/Name").GetComponent<Text>().text.Equals(""))
               {
                  Debug.Log("敌人打死玩家");
                  
                  //显示游戏结束，告诉服务器结算结果
                  PlayerManage.instance.GameEnd();
                  
               
               }
               else
               {
                  Debug.Log("敌人打死敌人");
                  //被打死的敌人销毁
                  PlayerManage.instance.PlyerDic.Remove(
                     collider.transform.Find("Health/Name").GetComponent<Text>().text);
                  
                  //销毁被打死玩家
                  Destroy(collider.gameObject);
               }
               
               } 
            
            
         }
         //隐藏子弹避免对不同玩家造成伤害
         gameObject.SetActive(false);
      }
   }
}
