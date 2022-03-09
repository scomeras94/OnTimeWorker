using OnTimeWorker.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnTimerWorker.Library.FrontServices
{
    public class PaginattionService
    {
        // Comuns
        public bool IsPaginattionNeeded(int maxPagItems, List<Register> registers = null, List<RegisterData> dataRegisters = null)
        {
            if (maxPagItems <= 0)
                return false; // throw ex

            if (registers != null)
            {
                if (registers.Count > maxPagItems)
                    return true;

                return false;
            }

            if (dataRegisters != null)
            {
                if (dataRegisters.Count > maxPagItems)
                    return true;

                return false;
            }

            return false; // throw ex double null
        }
        public int GetPaginattionRange(int maxPagItems, List<Register> registers = null, List<RegisterData> dataRegisters = null)
        {
            if (maxPagItems <= 0)
                return -1; // throw ex

            if (registers != null)
            {
                if (registers.Count > maxPagItems)
                    return maxPagItems;

                return registers.Count;
            }

            if (dataRegisters != null)
            {
                if (dataRegisters.Count > maxPagItems)
                    return maxPagItems;

                return dataRegisters.Count;
            }

            return -1; // throw ex double null
        }
        
        // DataRegisters
        public List<RegisterData> GetRestOfDataRegisters(int index, int count, List<RegisterData> dataRegisters)
        {
            if (index < 0)
                return null;
            if (count < 0)
                return null;
            if (dataRegisters == null || dataRegisters.Count == 0)
                return null;

            List<RegisterData> restOfDataRegisters = new List<RegisterData>();
            for (int i = index; i < count; i++)
            {
                restOfDataRegisters.Add(dataRegisters[i]);
            }

            return restOfDataRegisters;
        }
        public List<RegisterData> AddIncludedDataRegisters(List<RegisterData> includedDataRegisters, List<RegisterData> pagDataRegisters)
        {
            if (includedDataRegisters == null || includedDataRegisters.Count == 0)
                return null;

            if (pagDataRegisters == null)
                pagDataRegisters = new List<RegisterData>();

            foreach (var i in includedDataRegisters)
            {
                pagDataRegisters.Add(i);
            }

            return pagDataRegisters;
        }

        // Registers
        public List<Register> GetRestOfRegisters(int index, int count, List<Register> registers)
        {
            if (index < 0)
                return null;
            if (count < 0)
                return null;
            if (registers == null || registers.Count == 0)
                return null;

            List<Register> restOfRegisters = new List<Register>();
            for (int i = index; i < count; i++)
            {
                restOfRegisters.Add(registers[i]);
            }

            return restOfRegisters;
        }
        public List<Register> AddIncludedRegisters(List<Register> includedRegisters, List<Register> pagRegisters)
        {
            if (includedRegisters == null || includedRegisters.Count == 0)
                return null;

            if (pagRegisters == null)
                pagRegisters = new List<Register>();

            foreach (var i in includedRegisters)
            {
                pagRegisters.Add(i);
            }

            return pagRegisters;
        }
    }
}
