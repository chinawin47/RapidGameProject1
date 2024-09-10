using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int scoreValue; // คะแนนเริ่มต้น
    Text score; // UI สำหรับแสดงคะแนน

    // ฟังก์ชันสำหรับเพิ่มคะแนน
    void Start()
    {
        score = GetComponent<Text>();
    }

    // ฟังก์ชันสำหรับอัพเดทข้อความใน UI
    void Update()
    {
        score.text = "Score: " + scoreValue;
    }
}
