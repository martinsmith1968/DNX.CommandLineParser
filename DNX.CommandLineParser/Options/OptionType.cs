namespace DNX.CommandLineParser.Options
{
    public enum OptionType
    {
        [DisplayOrder(10)]
        Parameter = 1,

        [DisplayOrder(20)]
        Option = 2,

        [DisplayOrder(30)]
        Switch = 3,
    }
}
