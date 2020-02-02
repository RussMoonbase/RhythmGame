using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelegramSource : MonoBehaviour {
    public static TelegramSource inst { get; private set; }

    private static string LOVE_POEM_PREFIX = "Dearest " + Names.LOVE_INTEREST + ",\n\n";
    private static string LOVE_POEM_SUFFIX = "\n\nYours always,\n" + Names.MAIN_CHARACTER + "\n";

    public TextAsset lovePoemCorpus;
    public TextAsset lawBookCorpus;

    public int minChainLength = 10;
    public int maxChainLength = 30;

    private MarkovChain lovePoemChain;
    private MarkovChain lawBookChain;

    private void Awake() {
        inst = this;
        DontDestroyOnLoad(this);

        MarkovChain.Config config = new MarkovChain.Config();
        config.minChainLength = this.minChainLength;
        config.maxChainLength = this.maxChainLength;
        config.random = RandomSource.rand;

        this.lovePoemChain = new MarkovChain(this.lovePoemCorpus, config);
        this.lawBookChain = new MarkovChain(this.lawBookCorpus, config);

        this.Regenerate();
    }

    public List<string> GetLovePoem() {
        List<string> finalPoem = new List<string>();

        finalPoem.AddRange(LOVE_POEM_PREFIX.Split(' '));
        finalPoem.AddRange(this.lovePoemChain.GetOutput());
        finalPoem.AddRange(LOVE_POEM_SUFFIX.Split(' '));

        return finalPoem;
    }

    public List<string> GetLawBook() {
        return this.lawBookChain.GetOutput();
    }

    public void Regenerate() {
        this.lovePoemChain.Regenerate();
        this.lawBookChain.Regenerate();

        Debug.Log(string.Join(" ", this.GetLovePoem()));
        Debug.Log(string.Join(" ", this.GetLawBook()));
    }
}
