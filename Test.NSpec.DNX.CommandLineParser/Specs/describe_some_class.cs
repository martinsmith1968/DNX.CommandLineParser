using NSpec;
using NSpec.Assertions;

namespace ClassLibrary2
{
    class describe_some_class : nspec
    {
        void given_true_is_true()
        {
            it["true is true"] = () => true.ShouldBeTrue();
        }
    }
}
