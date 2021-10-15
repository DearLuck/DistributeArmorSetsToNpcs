using System.Linq;
using RecombinePatch.Analysis.Chance;
using FluentAssertions;
using NUnit.Framework;

namespace DearLuck.AnalysisTests
{
    public class ChancesTests
    {
        [Test]
        public void FirstItemNull()
        {
            var item = new LeveledDrops(new LevelRange(null, null), 1.0f);
            item.Items.Should().HaveCount(1);
            item.Items.Should().ContainKey(new LevelRange(null, null));
            item.Items.Values.Single().Should().Be(1.0f);
        }
        
        [Test]
        public void FirstItemRange()
        {
            var item = new LeveledDrops(new LevelRange(1, 8), 1.0f);
            item.Items.Should().HaveCount(1);
            item.Items.Should().ContainKey(new LevelRange(1, 8));
            item.Items.Values.Single().Should().Be(1.0f);
        }
        
        [Test]
        public void FirstItemRangeLh()
        {
            var item = new LeveledDrops(new LevelRange(1, null), 1.0f);
            item.Items.Should().HaveCount(1);
            item.Items.Should().ContainKey(new LevelRange(1, null));
            item.Items.Values.Single().Should().Be(1.0f);
        }
        
        [Test]
        public void FirstItemRangeUh()
        {
            var item = new LeveledDrops(new LevelRange(null, 8), 1.0f);
            item.Items.Should().HaveCount(1);
            item.Items.Should().ContainKey(new LevelRange(null, 8));
            item.Items.Values.Single().Should().Be(1.0f);
        }
        
        [Test]
        public void NullRangePlusNullRange()
        {
            var item = new LeveledDrops(new LevelRange(null, null), 1.0f);
            item.Add(new LevelRange(null, null), 0.5f);
            item.Items.Should().HaveCount(1);
            item.Items.Should().ContainKey(new LevelRange(null, null));
            item.Items.Values.Single().Should().Be(1.5f);
        }
        
        [Test]
        public void ExactRangePlusSameRange()
        {
            var item = new LeveledDrops(new LevelRange(1, 2), 1.0f);
            item.Add(new LevelRange(1, 2), 0.5f);
            item.Items.Should().HaveCount(1);
            item.Items.Should().ContainKey(new LevelRange(1, 2));
            item.Items.Values.Single().Should().Be(1.5f);
        }
        
        [Test]
        public void AddNonOverlappingStart()
        {
            var item = new LeveledDrops(new LevelRange(5, null), 1.0f);
            item.Add(new LevelRange(null, 5), 0.5f);
            
            item.Items.Should().HaveCount(2);
            item.Items[new LevelRange(null, 5)].Should().Be(0.5f);
            item.Items[new LevelRange(5, null)].Should().Be(1.0f);
        }
        
        [Test]
        public void AddNonOverlappingStartGap()
        {
            var item = new LeveledDrops(new LevelRange(5, null), 1.0f);
            item.Add(new LevelRange(null, 4), 0.5f);
            
            item.Items.Should().HaveCount(2);
            item.Items[new LevelRange(null, 4)].Should().Be(0.5f);
            item.Items[new LevelRange(5, null)].Should().Be(1.0f);
        }
        
        [Test]
        public void AddOverlappingStart()
        {
            var item = new LeveledDrops(new LevelRange(5, null), 1.0f);
            item.Add(new LevelRange(null, 6), 0.5f);
            
            item.Items.Should().HaveCount(3);
            item.Items[new LevelRange(null, 5)].Should().Be(0.5f);
            item.Items[new LevelRange(5, 6)].Should().Be(1.5f);
            item.Items[new LevelRange(6, null)].Should().Be(1.0f);
        }
        
        [Test]
        public void AddNonOverlappingEnd()
        {
            var item = new LeveledDrops(new LevelRange(null, 5), 1.0f);
            item.Add(new LevelRange(5, null), 0.5f);
            
            item.Items.Should().HaveCount(2);
            item.Items[new LevelRange(null, 5)].Should().Be(1.0f);
            item.Items[new LevelRange(5, null)].Should().Be(0.5f);
        }
        
        [Test]
        public void AddNonOverlappingEndGap()
        {
            var item = new LeveledDrops(new LevelRange(null, 5), 1.0f);
            item.Add(new LevelRange(6, null), 0.5f);
            
            item.Items.Should().HaveCount(2);
            item.Items[new LevelRange(null, 5)].Should().Be(1.0f);
            item.Items[new LevelRange(6, null)].Should().Be(0.5f);
        }
        
        [Test]
        public void AddOverlappingEnd()
        {
            var item = new LeveledDrops(new LevelRange(null, 5), 1.0f);
            item.Add(new LevelRange(4, null), 0.5f);
            
            item.Items.Should().HaveCount(3);
            item.Items[new LevelRange(null, 4)].Should().Be(1.0f);
            item.Items[new LevelRange(4, 5)].Should().Be(1.5f);
            item.Items[new LevelRange(5, null)].Should().Be(0.5f);
        }
        
        [Test]
        public void AddNonOverlappingMiddle()
        {
            var item = new LeveledDrops(new LevelRange(null, 5), 1.0f);
            item.Add(new LevelRange(10, null), 2.0f);
            
            item.Add(new LevelRange(5, 10), 0.5f);
            
            item.Items.Should().HaveCount(3);
            item.Items[new LevelRange(null, 5)].Should().Be(1.0f);
            item.Items[new LevelRange(5, 10)].Should().Be(0.5f);
            item.Items[new LevelRange(10, null)].Should().Be(2.0f);
        }
        
        [Test]
        public void AddOverlappingMiddle()
        {
            var item = new LeveledDrops(new LevelRange(null, 5), 1.0f);
            item.Add(new LevelRange(10, null), 2.0f);
            
            item.Add(new LevelRange(4, 11), 0.5f);
            
            item.Items.Should().HaveCount(5);
            item.Items[new LevelRange(null, 4)].Should().Be(1.0f);
            item.Items[new LevelRange(4, 5)].Should().Be(1.5f);
            item.Items[new LevelRange(5, 10)].Should().Be(0.5f);
            item.Items[new LevelRange(10, 11)].Should().Be(2.5f);
            item.Items[new LevelRange(11, null)].Should().Be(2.0f);
        }
        
        [Test]
        public void AddOverlappingMiddle2()
        {
            var item = new LeveledDrops(new LevelRange(7, 9), 1.0f);
            item.Add(new LevelRange(1, 3), 2f);
            
            item.Add(new LevelRange(2, 8), 0.5f);
            
            item.Items.Should().HaveCount(5);
            item.Items[new LevelRange(1, 2)].Should().Be(2.0f);
            item.Items[new LevelRange(2, 3)].Should().Be(2.5f);
            item.Items[new LevelRange(3, 7)].Should().Be(0.5f);
            item.Items[new LevelRange(7, 8)].Should().Be(1.5f);
            item.Items[new LevelRange(8, 9)].Should().Be(1.0f);
        }
        
        [Test]
        public void AddNonOverlappingMiddleGap()
        {
            var item = new LeveledDrops(new LevelRange(null, 5), 1.0f);
            item.Add(new LevelRange(10, null), 2.0f);
            
            item.Add(new LevelRange(6, 7), 0.5f);
            
            item.Items.Should().HaveCount(3);
            item.Items[new LevelRange(null, 5)].Should().Be(1.0f);
            item.Items[new LevelRange(6, 7)].Should().Be(0.5f);
            item.Items[new LevelRange(10, null)].Should().Be(2.0f);
        }
        
        [Test]
        public void NullRangePlusExactRange()
        {
            var item = new LeveledDrops(new LevelRange(null, null), 1.0f);
            item.Add(new LevelRange(5, 8), 0.5f);
            item.Items.Should().HaveCount(3);
            item.Items[new LevelRange(null, 5)].Should().Be(1.0f);
            item.Items[new LevelRange(5, 8)].Should().Be(1.5f);
            item.Items[new LevelRange(8, null)].Should().Be(1.0f);
        }
        
        [Test]
        public void ExactRangePlusNullRange()
        {
            var item = new LeveledDrops(new LevelRange(5, 8), 0.5f);
            
            item.Add(new LevelRange(null, null), 1.0f);
            
            item.Items.Should().HaveCount(3);
            item.Items[new LevelRange(null, 5)].Should().Be(1.0f);
            item.Items[new LevelRange(5, 8)].Should().Be(1.5f);
            item.Items[new LevelRange(8, null)].Should().Be(1.0f);
        }
        
        [Test]
        public void FewRangesPlusSameRange()
        {
            var item = new LeveledDrops(new LevelRange(1, 2), 1.0f);
            item.Add(new LevelRange(7, 8), 2f);
            
            item.Add(new LevelRange(1, 2), 0.5f);
            
            item.Items.Should().HaveCount(2);
            item.Items[new LevelRange(1, 2)].Should().Be(1.5f);
            item.Items[new LevelRange(7, 8)].Should().Be(2f);
        }
        
        [Test]
        public void FewRangesPlusSameRangeOl1()
        {
            var item = new LeveledDrops(new LevelRange(2, 4), 1.0f);
            item.Add(new LevelRange(7, 8), 2f);
            
            item.Add(new LevelRange(1, 4), 0.5f);
            
            item.Items.Should().HaveCount(3);
            item.Items[new LevelRange(1, 2)].Should().Be(0.5f);
            item.Items[new LevelRange(2, 4)].Should().Be(1.5f);
            item.Items[new LevelRange(7, 8)].Should().Be(2f);
        }
        
        [Test]
        public void FewRangesPlusSameRangeOl2()
        {
            var item = new LeveledDrops(new LevelRange(2, 4), 1.0f);
            item.Add(new LevelRange(7, 8), 2f);
            
            item.Add(new LevelRange(2, 5), 0.5f);
            
            item.Items.Should().HaveCount(3);
            item.Items[new LevelRange(2, 4)].Should().Be(1.5f);
            item.Items[new LevelRange(4, 5)].Should().Be(0.5f);
            item.Items[new LevelRange(7, 8)].Should().Be(2f);
        }
        
        [Test]
        public void FewRangesPlusSameRange2()
        {
            var item = new LeveledDrops(new LevelRange(7, 8), 1.0f);
            item.Add(new LevelRange(1, 2), 2f);
            
            item.Add(new LevelRange(7, 8), 0.5f);
            
            item.Items.Should().HaveCount(2);
            item.Items[new LevelRange(1, 2)].Should().Be(2.0f);
            item.Items[new LevelRange(7, 8)].Should().Be(1.5f);
        }
        
        [Test]
        public void FewRangesPlusSameRange2Ol1()
        {
            var item = new LeveledDrops(new LevelRange(7, 9), 1.0f);
            item.Add(new LevelRange(1, 2), 2f);
            
            item.Add(new LevelRange(8, 9), 0.5f);
            
            item.Items.Should().HaveCount(3);
            item.Items[new LevelRange(1, 2)].Should().Be(2.0f);
            item.Items[new LevelRange(7, 8)].Should().Be(1.0f);
            item.Items[new LevelRange(8, 9)].Should().Be(1.5f);
        }
        
        [Test]
        public void FewRangesPlusSameRange2Ol2()
        {
            var item = new LeveledDrops(new LevelRange(7, 9), 1.0f);
            item.Add(new LevelRange(1, 2), 2f);
            
            item.Add(new LevelRange(7, 8), 0.5f);
            
            item.Items.Should().HaveCount(3);
            item.Items[new LevelRange(1, 2)].Should().Be(2.0f);
            item.Items[new LevelRange(7, 8)].Should().Be(1.5f);
            item.Items[new LevelRange(8, 9)].Should().Be(1.0f);
        }
    }
}