using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static pnlGanhouCalculos;

public static class Extencao1
{

    public static IEnumerator AumentaXpGraduamente(this TMP_Text txt, AumentaDelegate aumentando, float start, float end, Image iconeLevel = null, Image imgFill = null, float limiteImgFill = 0f, float waitForSecondsForStart = 1f, float waitForSecondForSoma = 0.06f, float QntSoma = 2f)
    {
        
        if(txt != null) txt.text = $"{start.ToString("0")} / {limiteImgFill.ToString("0")}";

        yield return new WaitForSeconds(waitForSecondsForStart);

        while (float.Parse(txt.text.Split('/')[0].Trim()) < end)
        {

            yield return new WaitForSeconds(waitForSecondForSoma);

            //Somando Aqui
            float val = 0f;
            if (txt != null) val = float.Parse(txt.text.Split('/')[0].Trim());
            if (val == 0f) val = 1;

            var diferenca = (end - val);

            //TODO - implementar efeito de quanto maior o xp alvo maior o incremento
            QntSoma = 5;

            if (txt != null) txt.text = $"{(float.Parse(txt.text.Split('/')[0].Trim()) + QntSoma).ToString("0")} / {limiteImgFill.ToString("0")}";

            ///////\\\\\\

            if (imgFill != null) imgFill.fillAmount = (float)((float.Parse(txt.text.Split('/')[0].Trim()) + QntSoma) / limiteImgFill);

            if (txt != null && float.Parse(txt.text.Split('/')[0].Trim()) > end)
            {
                txt.text = $"{end.ToString("0")} / {limiteImgFill.ToString("0")}";

                if (imgFill != null) imgFill.fillAmount = end / limiteImgFill;
            }
        }

        if (txt != null && float.Parse(txt.text.Split('/')[0].Trim()) >= limiteImgFill)
        {
            if ((LevelManager.instancia.ExpEmJogo + LevelManager.instancia.ExpAtual) > LevelManager.instancia.ExpAlvo)
            {
                LevelManager.instancia.ExpEmJogo += LevelManager.instancia.ExpAtual;
                LevelManager.instancia.ExpEmJogo = LevelManager.instancia.ExpEmJogo - LevelManager.instancia.ExpAlvo;
                LevelManager.instancia.AumentaNivel();

                if (iconeLevel != null) iconeLevel.sprite = LevelManager.instancia.LevelSprites[LevelManager.instancia.LevelAtual - 1];
                imgFill.fillAmount = 0;

                aumentando(LevelManager.instancia.ExpEmJogo, LevelManager.instancia.ExpAlvo);
            }
        }
    }

    public static IEnumerator AumentaGradualmente(this TMP_Text txt, char caracterechave, float start, float end, float waitForSecondsForStart = 1f, float waitForSecondForSoma = 0.06f, float QntSoma = 2f)
    {
        string textoFixo = "";
        if (txt != null) textoFixo = txt.text.Split(caracterechave)[0];

        if(txt != null) txt.text = $"{textoFixo}{caracterechave} {start.ToString("0")}";

        yield return new WaitForSeconds(waitForSecondsForStart);
        while (txt != null && float.Parse(txt.text.Split(caracterechave)[1]) < end)
        {
            yield return new WaitForSeconds(waitForSecondForSoma);

            if (txt != null) txt.text = $"{textoFixo}{caracterechave} {(float.Parse(txt.text.Split(caracterechave)[1]) + QntSoma).ToString("0")}";

            if (txt != null && (float.Parse(txt.text.Split(caracterechave)[1]) > end))
            {
                txt.text.Split(caracterechave)[1] = end.ToString();
                txt.text = $"{textoFixo}{caracterechave} {end.ToString("0")}";
            }
        }
    }

    public static IEnumerator AumentaGradualmente(this TMP_Text txt, float start, float end, float waitForSecondsForStart = 1f, float waitForSecondForSoma = 0.06f, float QntSoma = 2f)
    {
        if (txt != null) txt.text = start.ToString("0");

        yield return new WaitForSeconds(waitForSecondsForStart);
        while (txt != null && float.Parse(txt.text) < end)
        {
            yield return new WaitForSeconds(waitForSecondForSoma);
            txt.text = (float.Parse(txt.text) + QntSoma).ToString("0");

            if (txt != null && float.Parse(txt.text) > end)
            {
                txt.text = end.ToString();
            }
        }
    }

    public static IEnumerator AumentaGradualmente(this Text txt, float start, float end, float waitForSecondsForStart = 1f, float waitForSecondForSoma = 0.06f, float QntSoma = 2f)
    {
      if(txt != null) txt.text = start.ToString("0");

        yield return new WaitForSeconds(waitForSecondsForStart);
        while (txt != null && float.Parse(txt.text) < end)
        {
            yield return new WaitForSeconds(waitForSecondForSoma);
            if(txt != null) txt.text = (float.Parse(txt.text) + QntSoma).ToString("0");

            if (txt != null && float.Parse(txt.text) > end)
            {
                txt.text = end.ToString();
            }
        }
    }

    public static void FlipParaTardis(this GameObject gObject, bool Scale)
    {
        if (Player.instancia == null) return;

        var tardis = Player.instancia.gameObject;

        if (Scale)
        {
            if (gObject.transform.position.x < tardis.transform.position.x)
            {
                var scale = gObject.transform.localScale;
                scale.x = Math.Abs(scale.x) * -1;
                gObject.transform.localScale = scale;
                //return false;
            }
            else if (gObject.transform.position.x > tardis.transform.position.x)
            {
                var scale = gObject.transform.localScale;
                scale.x = Math.Abs(scale.x);
                gObject.transform.localScale = scale;

                //return true;
            }
        }
        else
        {
            if (gObject.transform.position.x < tardis.transform.position.x)
            {
                var scale = gObject.transform.localScale;
                scale.x = Math.Abs(scale.x);
                gObject.transform.localScale = scale;
                //return false;
            }
            else if (gObject.transform.position.x > tardis.transform.position.x)
            {
                var scale = gObject.transform.localScale;
                scale.x = Math.Abs(scale.x) * -1;
                gObject.transform.localScale = scale;

                //return true;
            }
        }
    }

    public static void ModificarEscalaX(this GameObject @object, float scaleX)
    {
        var scale = @object.transform.localScale;
        scale.x = scaleX;
        @object.transform.localScale = scale;
    }

    public static void ModificarEscalaX(this GameObject @object, float scaleX, bool manterValor = false)
    {
        var scale = @object.transform.localScale;
        scale.x = (manterValor ? (scaleX > 0 ? Math.Abs(scale.y) : Math.Abs(scale.y) * -1) : (scaleX));
        @object.transform.localScale = scale;
    }

    public static void ModificarEscalaY(this GameObject @object, float scaleY, bool manterValor = false)
    {
        var scale = @object.transform.localScale;
        scale.y = (manterValor ? (scaleY > 0 ? Math.Abs(scale.y) : Math.Abs(scale.y) * -1) : (scaleY));
        @object.transform.localScale = scale;
    }

}

