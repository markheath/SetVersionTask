using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SetVersionTask;

namespace SetVersionTaskTests
{
    [TestFixture]
    public class VersionUpdateRuleTests
    {
        [TestCase("1")]
        [TestCase("")]
        [TestCase("1.")]
        public void RulesMustHaveAtLeastTwoParts(string rule)
        {
            try
            {
                new VersionUpdateRule(rule);
            }
            catch (ArgumentException)
            {
                // expected
            }
            catch (FormatException)
            {
                // also expected
            }
        }
        
        [TestCase("1.2.3.4.5")]
        [TestCase("......")]
        [TestCase("1.2.3.4.")]
        public void RulesMustHaveNoMoreThanFourParts(string rule)
        {
            Assert.Throws<ArgumentException>(() => new VersionUpdateRule(rule));
        }
    
    }
}
