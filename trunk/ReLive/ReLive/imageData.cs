class imageData
{
    private string fn;
    private string fp;
    public imageData(string _fn, string _fp)
    {
        fn = _fn;
        fp = _fp;
    }
    public string fileName
    {
        get
        {
            return fn;
        }
        set
        {
        }
    }
    
    public string filePath
    {
        get
        {
            return fp;
        }
        set
        {
        }
    }

    public override string ToString()
    {
        return fn;
    }
}
