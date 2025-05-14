
// Type: game.SeperatedAxesList
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using System;
using System.Collections.Generic;

#nullable disable
namespace game
{
  public class SeperatedAxesList
  {
    private List<SeperatedAxis> m_axisList;
    private int m_axisNum;

    public SeperatedAxesList()
    {
      this.m_axisList = new List<SeperatedAxis>();
      this.m_axisNum = 0;
    }

    public void Destructor()
    {
      foreach (SeperatedAxis axis in this.m_axisList)
        axis.Destructor();
      this.m_axisList.Clear();
      this.m_axisList = (List<SeperatedAxis>) null;
    }

    public void reset() => this.m_axisNum = 0;

    public SeperatedAxis getAxis(int index) => this.m_axisList[index];

    public int getAxisNum() => this.m_axisNum;

    private SeperatedAxis newAxis()
    {
      SeperatedAxis seperatedAxis;
      if (this.m_axisNum == this.m_axisList.Count)
      {
        seperatedAxis = new SeperatedAxis();
        this.m_axisList.Add(seperatedAxis);
      }
      else
        seperatedAxis = this.m_axisList[this.m_axisNum];
      ++this.m_axisNum;
      return seperatedAxis;
    }

    public void addOrthogonalAxes(CollShape shape)
    {
      MathOrthoBox bounds = shape.getBounds();
      MathVector direction = new MathVector(1f, 0.0f, 0.0f);
      this.newAxis().setAxis(direction, 0, bounds.min.x, bounds.max.x, SeperatedAxis.AxisType.AXIS_X);
      direction.set(0.0f, 1f, 0.0f);
      this.newAxis().setAxis(direction, 0, bounds.min.y, bounds.max.y, SeperatedAxis.AxisType.AXIS_Y);
      direction.set(0.0f, 0.0f, 1f);
      this.newAxis().setAxis(direction, 0, bounds.min.z, bounds.max.z, SeperatedAxis.AxisType.AXIS_Z);
    }

    public void addAxis(MathVector direction, int shapeIndex, float minT, float maxT)
    {
      this.newAxis().setAxis(direction, shapeIndex, minT, maxT);
    }

    public void addAxis(SeperatedAxis axisToAdd) => this.newAxis().setAxis(axisToAdd);

    public void addAxes(SeperatedAxesList otherAxes)
    {
      for (int index = 0; index != otherAxes.m_axisNum; ++index)
        this.newAxis().setAxis(otherAxes.getAxis(index));
    }

    public bool noIntersection(int shape1, int shape2)
    {
      for (int index = 0; index != this.m_axisNum; ++index)
      {
        if (this.m_axisList[index].noIntersection(shape1, shape2))
          return true;
      }
      return false;
    }

    public void combineTValues()
    {
      for (int index = 0; index != this.m_axisNum; ++index)
      {
        SeperatedAxis axis = this.m_axisList[index];
        axis.setTValues(2, Math.Min(axis.getMinT(1), axis.getMinT(2)), Math.Max(axis.getMaxT(1), axis.getMaxT(2)));
      }
    }

    public void addRangeToAxesNoCheck(CollShape shape, int shapeNum)
    {
      for (int index = 0; index != this.m_axisNum; ++index)
        shape.setRangeOnAxis(this.m_axisList[index], shapeNum);
    }

    public int addRangeToAxesWithCheck(CollShape shape, int shapeNum)
    {
      for (int index = 0; index != this.m_axisNum; ++index)
      {
        SeperatedAxis axis = this.m_axisList[index];
        shape.setRangeOnAxis(axis, shapeNum);
        if (axis.noIntersection())
          return index;
      }
      return -1;
    }

    public static void getMoveOutVector(
      SeperatedAxesList list1,
      SeperatedAxesList list2,
      int staticShapeIndex,
      ref MathVector moveVector)
    {
      float num1 = 9999999f;
      float moveDist = 0.0f;
      MathVector moveVector1 = new MathVector();
      int num2 = Math.Min(list1.m_axisNum, 3);
      List<SeperatedAxis> axisList1 = list1.m_axisList;
      for (int index = 0; index != num2; ++index)
      {
        if (axisList1[index].getMoveOutVector(staticShapeIndex, ref moveDist, ref moveVector1) && (double) Math.Abs(moveDist) < (double) num1)
        {
          num1 = Math.Abs(moveDist);
          moveVector.set(moveVector1);
        }
      }
      int axisNum = list2.m_axisNum;
      List<SeperatedAxis> axisList2 = list2.m_axisList;
      for (int index = 0; index != axisNum; ++index)
      {
        if (axisList2[index].getMoveOutVector(staticShapeIndex, ref moveDist, ref moveVector1) && (double) Math.Abs(moveDist) < (double) num1)
        {
          num1 = Math.Abs(moveDist);
          moveVector.set(moveVector1);
        }
      }
    }

    public static void getMoveOutMultiple(
      SeperatedAxesList list1,
      SeperatedAxesList list2,
      MathVector move,
      int shapeIndex,
      ref float moveOutMultiple,
      ref SeperatedAxis axisMove)
    {
      moveOutMultiple = -9999999f;
      int num = Math.Min(list1.m_axisNum, 3);
      List<SeperatedAxis> axisList1 = list1.m_axisList;
      for (int index = 0; index != num; ++index)
      {
        SeperatedAxis seperatedAxis = axisList1[index];
        float moveOutMultiple1 = seperatedAxis.getMoveOutMultiple(move, shapeIndex);
        if ((double) moveOutMultiple <= (double) moveOutMultiple1)
        {
          moveOutMultiple = moveOutMultiple1;
          axisMove = seperatedAxis;
        }
      }
      int axisNum = list2.m_axisNum;
      List<SeperatedAxis> axisList2 = list2.m_axisList;
      for (int index = 0; index != axisNum; ++index)
      {
        SeperatedAxis seperatedAxis = axisList2[index];
        float moveOutMultiple2 = seperatedAxis.getMoveOutMultiple(move, shapeIndex);
        if ((double) moveOutMultiple <= (double) moveOutMultiple2)
        {
          moveOutMultiple = moveOutMultiple2;
          axisMove = seperatedAxis;
        }
      }
    }

    public static void storeStaticBounds(SeperatedAxesList srcList, SeperatedAxesList destList)
    {
      int axisNum = srcList.m_axisNum;
      List<SeperatedAxis> axisList1 = srcList.m_axisList;
      List<SeperatedAxis> axisList2 = destList.m_axisList;
      for (int index = 0; index != axisNum; ++index)
      {
        float[] tvalues1 = axisList1[index].getTValues(1);
        float[] tvalues2 = axisList2[index].getTValues(2);
        tvalues2[0] = tvalues1[0];
        tvalues2[1] = tvalues1[1];
      }
    }

    public static bool checkCombineAxes(
      SeperatedAxesList checkList,
      CollShape movingShape,
      CollShape storedShape,
      CollShape staticShape,
      ref SeperatedAxis combineAxis,
      bool orthagonalOnly,
      MathVector move)
    {
      int axisNum = checkList.getAxisNum();
      for (int index = 0; index != axisNum; ++index)
      {
        SeperatedAxis axis = checkList.getAxis(index);
        storedShape?.setRangeOnAxis(axis, 2);
        staticShape?.setRangeOnAxis(axis, 1);
        float minT1 = axis.getMinT(1);
        float maxT1 = axis.getMaxT(1);
        float minT2 = axis.getMinT(2);
        float maxT2 = axis.getMaxT(2);
        if (GameCommon.compareFloats(minT1, maxT2) || GameCommon.compareFloats(maxT1, minT2))
        {
          if (combineAxis != null)
            return false;
          combineAxis = axis;
        }
        else
        {
          movingShape?.setRangeOnAxis(axis, 0);
          if (!orthagonalOnly || 3 > index)
          {
            float minT3 = axis.getMinT(0);
            float maxT3 = axis.getMaxT(0);
            float num1 = (float) (((double) minT1 + (double) maxT1) * 0.5);
            float num2 = (float) (((double) minT2 + (double) maxT2) * 0.5);
            float num3 = (float) (((double) minT3 + (double) maxT3) * 0.5);
            if ((double) num3 < (double) num1 && (double) num3 < (double) num2)
            {
              if (!GameCommon.compareFloats(minT1, minT2))
                return false;
            }
            else if ((double) num1 < (double) num3 && (double) num2 < (double) num3)
            {
              if (!GameCommon.compareFloats(maxT1, maxT2))
                return false;
            }
            else if ((double) minT1 - 0.0099999997764825821 >= (double) minT3 || (double) maxT3 >= (double) maxT1 + 0.0099999997764825821 || (double) minT2 - 0.0099999997764825821 >= (double) minT3 || (double) maxT3 >= (double) maxT2 + 0.0099999997764825821)
              return false;
          }
        }
      }
      return true;
    }
  }
}
