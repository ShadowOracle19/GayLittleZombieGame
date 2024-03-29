using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseEncounter : ScriptableObject
{
    public RewardSystem reward;

    public bool isTutorial;
    

    public virtual void StartEncounter()
    {
        Debug.Log("Base Encounter");
        return;
    }

    public virtual void GiveReward()
    {
        if (GameManager.Instance.node.encounter.reward == null) return;
        GameManager.Instance.popupParent.gameObject.GetComponent<DeleteRewardPopup>().rewardDisplayed = true;
        SoundEffectManager.Instance.rewardSFX.Play();
        if (reward.numberOfAddedMedkits > 0)
        {
            GameManager.Instance.amountOfMedkits += reward.numberOfAddedMedkits;
            var popup = Instantiate(GameManager.Instance.rewardBasePopup, GameManager.Instance.popupParent);
            popup.GetComponent<RewardPopup>().whatRewardTextSays = reward.numberOfAddedMedkits + " Medkits";
            popup.GetComponent<RewardPopup>().rewardIcon.sprite = GameManager.Instance.medkitSprite;
        }
        if (reward.numberOfAddedCaps > 0)
        {
            GameManager.Instance.caps += reward.numberOfAddedCaps;
            var popup = Instantiate(GameManager.Instance.rewardBasePopup, GameManager.Instance.popupParent);
            popup.GetComponent<RewardPopup>().whatRewardTextSays = reward.numberOfAddedCaps + " Caps";
            popup.GetComponent<RewardPopup>().rewardIcon.sprite = GameManager.Instance.capSprite;
        }
        if (reward.numberOfAddedRations > 0)
        {
            GameManager.Instance.amountOfRations += reward.numberOfAddedRations;
            var popup = Instantiate(GameManager.Instance.rewardBasePopup, GameManager.Instance.popupParent);
            popup.GetComponent<RewardPopup>().whatRewardTextSays = reward.numberOfAddedRations + " Rations";
            popup.GetComponent<RewardPopup>().rewardIcon.sprite = GameManager.Instance.rationsSprite;
        }
        if(reward.tokensToModify.Length > 0)
        {
            foreach (var token in reward.tokensToModify)
            {
                var popup = Instantiate(GameManager.Instance.rewardBasePopup, GameManager.Instance.popupParent);
                popup.GetComponent<RewardPopup>().whatRewardTextSays = token.tokenToModify.name;
                token.tokenToModify.damageAmount += token.newDamageAmount;
                token.tokenToModify.healingAmount += token.newHealingAmount;
                token.tokenToModify.defendAmount += token.newDefendAmount;

                if(token.newDamageAmount > 0)
                {
                    popup.GetComponent<RewardPopup>().whatRewardTextSays += " +" + token.newDamageAmount + " damage";
                }
                if(token.newHealingAmount > 0)
                {
                    popup.GetComponent<RewardPopup>().whatRewardTextSays += " +" + token.newHealingAmount + " healing";

                }
                if(token.newHealingAmount > 0)
                {
                    popup.GetComponent<RewardPopup>().whatRewardTextSays += " +" + token.newDefendAmount + " defend";

                }

                if (token.newUser != null) token.tokenToModify.character = token.newUser;
                if (token.newSprite != null) token.tokenToModify.icon = token.newSprite;

                 token.tokenToModify.icon = popup.GetComponent<RewardPopup>().rewardIcon.sprite;
            }
        }
    }
}
