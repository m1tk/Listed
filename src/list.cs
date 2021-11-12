using System;

namespace list {
    class Node<A> where A: IComparable {
        private A val;
        private Node<A> next;

        public void set_val(A mval) {
            this.val = mval;
        }

        public A get_val() {
            return this.val;
        }

        public Node<A> get_next() {
            return this.next;
        }

        public void set_next(Node<A> nex) {
            this.next = nex;
        }
    }

    static class ListConst {
        public const int LastItem = -1;
    }

    public class List<A>: IComparable<List<A>> where A: IComparable {
        private int len;
        private Node<A> start;

        /// Constructor of List
        /// Dimension is the initial size of the list
        public List(int dim) {
            Node<A> iter, _new;
            len = dim;

            if (dim <= 0) {
                start = null;
                return;
            }

            start = new Node<A>();
            iter  = start;

            if (dim == 1) {
                start.set_next(null);
                return;
            }

            for (int i = 1; i < len; i++) {
                _new = new Node<A>();
                iter.set_next(_new);
                iter = _new;
            }
            iter.set_next(null);
        }

        /// Get List length
        public int length() {
            return this.len;
        }

        /// Inner method that checks if index exists
        private void check_range(int index) {
            if (this.len-1 < index) {
                throw new System.IndexOutOfRangeException("Index is out of range");
            }
        }

        /// Set value for index
        /// Index must exist in range
        /// Can throw System.IndexOutOfRangeException if index is out of range
        public void set(int index, A val) {
            this.check_range(index);
            Node<A> iter = this.start;

            for (int i = 0; i <= index; i++) {
                if (i == index) {
                    iter.set_val(val);
                }
                iter = iter.get_next();
            }
        }

        /// Insert element to list
        /// Insertion to the end can be specified with const LastItem
        /// Can throw System.IndexOutOfRangeException if index > 0 and index-1 doesn't exist
        public void insert(int index, A val) {
            if (index != ListConst.LastItem)
                this.check_range(index-1);

            Node<A> _new = new Node<A>();
            _new.set_val(val);
            _new.set_next(null);

            if (index == 0 || this.len == 0) {
                _new.set_next(this.start);
                this.start = _new;
                this.len += 1;
                return;
            }

            Node<A> iter, prev;
            iter = this.start;
            prev = null;
            for (int i = 0; i < this.len; i++) {
                if (index == i) {
                    _new.set_next(iter);
                    prev.set_next(_new);
                    this.len += 1;
                    return;
                }
                prev = iter;
                if (i < this.len-1)
                    iter = iter.get_next();
            }
            this.len += 1;
            // insertion to tail
            iter.set_next(_new);
        }

        /// Get index value from List
        /// Can throw System.IndexOutOfRangeException if index is out of range
        public A get(int index) {
            this.check_range(index);
            Node<A> iter = this.start;
            
            for (int i = 0; i <= index; i++) {
                if (i == index) {
                    return iter.get_val();
                }
                iter = iter.get_next();
            }

            return this.start.get_val();
        }

        /// Delete value from list by index
        /// This changes list length
        /// Can throw System.IndexOutOfRangeException if index is out of range
        public void del_index(int index) {
            this.check_range(index);

            // if element is head, remove it
            // and head is next element
            if (index == 0) {
                this.start = this.start.get_next();
                this.len  -= 1;
                return;
            }

            Node<A> iter, prev;
            iter = this.start;
            prev = null;
            // otherwise iterate till we find our element
            for (int i = 0; i <= index; i++) {
                if (i == index) {
                    // Remove item with wanted index
                    // from:
                    //  prev -x-> target -x-> next
                    // to:
                    // prev -> next
                    prev.set_next(iter.get_next());
                    this.len -= 1;
                    return;
                }
                prev = iter;
                iter = iter.get_next();
            }
        }

        /// Delete value from list by comparison
        /// This changes list length
        public void del_val(A val) {
            // if val matches the one in head, remove it
            // and head is next element
            if (val.Equals(this.start.get_val())) {
                this.start = this.start.get_next();
                this.len  -= 1;
                return;
            }

            Node<A> iter, prev;
            iter = this.start;
            prev = null;
            // iterate till the end and remove all matches
            while(true) {
                if (val.Equals(iter.get_val())) {
                    // Remove matching item
                    // from:
                    //  prev -x-> target -x-> next
                    // to:
                    // prev -> next
                    prev.set_next(iter.get_next());
                    this.len -= 1;
                    // in this case prev is still the same
                    // but iter will be next
                    iter = prev.get_next();
                } else {
                    prev = iter;
                    iter = iter.get_next();
                }
                if (iter == null)
                    break;
            }
        }

        /// Find first matching element index in list
        /// returns -1 if no element found
        public int find(A val) {
            Node<A> iter = this.start;

            for (int i = 0; i < this.len; i++) {
                if (val.Equals(iter.get_val())) {
                    return i;
                }
                iter = iter.get_next();
            }
            return -1;
        }

        /// Find matching elements indexes in list
        /// Empty list returned if none exist
        public List<int> find_all(A val) {
            List<int> matches = new List<int>(0);
            Node<A> iter = this.start;

            for (int i = 0; i < this.len; i++) {
                if (val.Equals(iter.get_val())) {
                    matches.insert(ListConst.LastItem, i);
                }
                iter = iter.get_next();
            }
            return matches;
        }

        /// Check if list is symetric
        /// Warning: expensive operation required
        public bool is_symetric() {
            Node<A> iter = this.start;
            Node<A> riter;
            
            // create inversed list of half of our list
            List<A> inverse = new List<A>(0);
            for (int c = 0; c < this.len/2; c++) {
                inverse.insert(0, iter.get_val());
                iter = iter.get_next();
            }
            riter = inverse.start;

            Console.WriteLine(this);
            Console.WriteLine(inverse);
            for (int i = 0; i < inverse.len; i++) {
                if (!riter.get_val().Equals(iter.get_val()))
                    return false;
                riter = riter.get_next();
                iter  = iter.get_next();
            }

            return true;
        }

        /// Sort list
        public void sort() {
            Node<A> iter = this.start;
            A temp_elem;
            for (int i = 0; i < this.len; i++) {
                while(iter.get_next() != null) {
                    if (iter.get_val().CompareTo(iter.get_next().get_val()) == 1) {
                        temp_elem = iter.get_val();
                        iter.set_val(iter.get_next().get_val());
                        iter.get_next().set_val(temp_elem);
                    }
                    iter = iter.get_next();
                }
                iter = this.start;
            }
        }
        
        /// Check if list is sorted
        public bool is_sorted() {
            Node<A> iter = this.start.get_next();
            A temp_elem  = this.start.get_val();
            while(iter != null) {
                if (temp_elem.CompareTo(iter.get_val()) == 1) {
                    return false;
                }
                temp_elem = iter.get_val();
                iter      = iter.get_next();
            }
            return true;
        }

        /// Concatenate two lists of same type
        public List<A> concat(List<A> rhs) {
            Node<A> iter     = this.start;
            List<A> _new     = new List<A>(this.len+rhs.len);
            Node<A> new_iter = _new.start;
            for (int i = 0; i < this.len; i++) {
                new_iter.set_val(iter.get_val());
                iter     = iter.get_next();
                new_iter = new_iter.get_next();
            }
            iter = rhs.start;
            for (int i = 0; i < rhs.len; i++) {
                new_iter.set_val(iter.get_val());
                iter     = iter.get_next();
                new_iter = new_iter.get_next();
            }

            return _new;
        }

        /// Fusing two sorted lists
        public List<A> sorted_fuse(List<A> rhs) {
            // sort first
            this.sort();
            rhs.sort();

            Node<A> iter  = this.start;
            Node<A> iter1 = rhs.start;
            List<A> res   = new List<A>(0);

            while (iter != null || iter1 != null) {
                if (iter != null && (iter1 == null || iter.get_val().CompareTo(iter1.get_val()) <= 0)) {
                    // in this case first list (iter) has smaller value or equal or rhs is finished
                    res.insert(ListConst.LastItem, iter.get_val());
                    // next value of first list
                    iter = iter.get_next();
                } else {
                    // otherwise rhs value will be put
                    res.insert(ListConst.LastItem, iter1.get_val());
                    iter1 = iter1.get_next();
                }
            }
            return res;
        }

        /// Display
        public override string ToString() {
            string list = "[";
            Node<A> iter = this.start;
            for (int i = 0; i < this.len; i++) {
                list += String.Format("{0}:{1}", i, iter.get_val());
                if (i != this.len-1)
                    list += ", ";
                iter = iter.get_next();
            }
            list += "]";
            return list;
        }

        /// Comparing list to other list
        public int CompareTo(List<A> rhs) {
            // no comparison return -1 if this size smaller than rhs 
            if (this.len != rhs.len)
                return (this.len < rhs.len ? -1 : 1);

            // now same size
            Node<A> iter  = this.start;
            Node<A> iter1 = rhs.start;
            int comp;
            while(iter != null) {
                comp = iter.get_val().CompareTo(iter1.get_val());
                if (comp != 0) {
                    return comp;
                }
                iter  = iter.get_next();
                iter1 = iter1.get_next();
            }
            return 0;
        }
    }
}
