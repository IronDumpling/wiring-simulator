using System;

public enum CheckResult{
    HugeFail,
    Fail,
    Success,
    HugeSuccess,
}

public struct CheckResultData{
    public int GenVal { get; }
    public int CheckVal { get; }
    public CheckResult Result { get; }

    public CheckResultData(int val1, int val2, CheckResult result){
        GenVal = val1;
        CheckVal = val2;
        Result = result;
    }

    public string PrintResult(){
        return "(" + GenVal + "/" + CheckVal + " " + Result.ToString() + ")";
    }
}
